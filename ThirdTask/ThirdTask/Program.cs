using Microsoft.VisualBasic;

namespace ThirdTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Создаём для тестов tableColumn и выводим
            TableColumn tableColumn = new TableColumn(0, "some value", true);
            TableColumn tableColumn2 = new TableColumn(10, "неограниченная вложенность", true);
            for (int i = 1; i < 10; i++)
            {
                tableColumn.addColumn(new TableColumn(i, $"value number {i}", Convert.ToBoolean(new Random().Next(0,2))) );
                tableColumn2.addColumn(new TableColumn(i+10, $"test number {i+10}", Convert.ToBoolean(new Random().Next(0,2))) );
            }
            tableColumn.addColumn(tableColumn2);
            Console.WriteLine(tableColumn.ToString());
            var keys_by_keys = tableColumn.GetKeys((x) => x % 2 == 0); //Четные ключи
            var keys_by_value = tableColumn.GetKeys((x) => x.Contains("test")); //Содержит слово тест
            var keys_by_isHeader = tableColumn.GetKeys((x) => x == true); //Только заголовочные

            Console.WriteLine("Четные ключи: \n" + ListToString(keys_by_keys));
            Console.WriteLine("\nСо словом \"Текст\": \n" + ListToString(keys_by_value));
            Console.WriteLine("\nЗаголовочные: \n" + ListToString(keys_by_isHeader));

        }

        static public string ListToString(List<int> list ) //Для удобства вывода результата
        {
            var result = String.Join(", ", list);
      
            return result;
        }


    }

    public class TableColumn
    {
        public int key; //Если айдишник, то можно как статик и при каждом новом будет +1, но не требуется
        public string value;
        public List<TableColumn> columns;
        public bool isHeader;
        
        //Конструктор
        public TableColumn(int key, string value, bool isHeader)
        {
            this.key = key;
            this.value = value;
            this.isHeader = isHeader;
            columns = new List<TableColumn>();
        }

        //Добавлять колонки
        public void addColumn(TableColumn column)
        {
            columns.Add(column);
        }

        //Делегаты для метода получения 
        public delegate bool isKey(int x);
        public delegate bool isHeaderState(bool x);
        public delegate bool isValue(string x);

        //Получать список при помощи делегатов, до этого таким не занимался - имеется вопрос по согласованию доступности делегата и методом
        public List<int> GetKeys(isKey func) //По int "key"
        {
            var result = new List<int>();

            if (func(this.key)) 
                result.Add(this.key);

            foreach (TableColumn column in this.columns)
            {  
                if (column.columns.Count > 0) //Проверка на неограниченную вложенность
                    result.AddRange(column.GetKeys(func));
                else
                    if (func(column.key))
                        result.Add(column.key);

            }

            return result;
        }

        public List<int> GetKeys(isHeaderState func) //По bool "isHeader"
        {
            var result = new List<int>();

            if (func(this.isHeader)) 
                result.Add(this.key);

            foreach (TableColumn column in this.columns)
            {
                if (column.columns.Count > 0) //Проверка на неограниченную вложенность
                    result.AddRange(column.GetKeys(func));
                else
                    if (func(column.isHeader))
                        result.Add(column.key);

            }

            return result;
        }

        public List<int> GetKeys(isValue func) //По string "value"
        {
            var result = new List<int>();

            if (func(this.value))
                result.Add(this.key);

            foreach (TableColumn column in this.columns)
            {
                if (column.columns.Count > 0) //Проверка на неограниченную вложенность
                    result.AddRange(column.GetKeys(func));
                else
                    if (func(column.value))
                        result.Add(column.key);

            }

            return result;
        }

        public override string? ToString() //Переопределяем для удобства
        {
            string returnString = $"TableColumn #{key}, value is '{value}', is header = {isHeader}.";
            if (columns.Count > 0)
            {
                returnString += "\n\tNested Columns:";
                foreach(TableColumn column in columns)
                {
                    returnString+= "\n\t-" + column.ToString();
                }
            }
            return returnString;
        }
    }
}