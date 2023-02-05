# Тестовые задания

Разделил по веткам:


1) Задания по [C#](https://github.com/vnikita8/SiberianGenericCompanyTest) - ветка [C#](https://github.com/vnikita8/SiberianGenericCompanyTest)


2) Задания по [SQL](https://github.com/vnikita8/SiberianGenericCompanyTest/tree/SQL) - ветка [SQL](https://github.com/vnikita8/SiberianGenericCompanyTest/tree/SQL)


Ниже будет код по каждому заданию ->

---

# Задание 1
Написать метод, который в качестве входных данных принимает массив букв английского алфавита по порядку и возвращает пропущенную букву в массиве.

```
//Решение 1 - чётко по заданию (без проверок, так как всё и так должно быть хорошо, только одна пропущенная)
        static string getMissing(string[] words)
        {
            string result = words[0];
            for (int i = 0;  i<words.Length-1; i++)
            {
                int char_select = (int)Char.Parse(words[i]);
                int char_next = (int)Char.Parse(words[i+1]);

                if ( char_next == char_select + 1)
                {
                    continue;
                } 
                else
                {
                    result =  ((char) (char_select + 1)).ToString();
                    break;
                }
            }

            return result;
        }


        //Решение 2 - с проверками на адекватность входного массива (пропущена не одна буква, только английский алфавит)
        static bool TryGetMissing(string[] words, out string out_string)
        {
            bool check_list = true; //Для проверки списка
            out_string = "Error Input"; //Дефолт строка для вывода

            for (int i = 0; i < words.Length - 1; i++) //Перебираем по индексам
            {
                int char_select = (int)Char.Parse(words[i]); //Перебираемая буква
                int char_next = (int)Char.Parse(words[i + 1]); //След символ
                
                if (char_next == char_select + 1) //Проверяем что буква идёт следующей
                {
                    continue;
                }
                else if (i < words.Length - 2 && char_next+1 == char_select + 2 && out_string != "Error Input") //Если пропущен не один символ
                {
                    out_string = ((char)(char_select + 1)).ToString();
                }
                else
                {
                    check_list = false;
                }
            }

            return check_list;
        }

```
---


# Задание 2 
Даны два числовых массива, необходимо написать LINQ-выражение, которое выдаст коллекцию из двух полей, где одно поле - это пересечение элементов массива, а другое поле - его квадрат.

```
static void LinqTask()
        {
            var firstList = Enumerable.Range(1,20); //Генерация от 1 до 20
            var secondList = Enumerable.Range(10, 6); //Генерация от 10 до 15

            // Перебираем пересечения и записываем в виде "Пересечение = x, квадрат пересечения = x^2"
            List<(int, int)> result = firstList.Intersect(secondList).Select(x => (x, (int) Math.Pow(x,2) ) ).ToList();

            //На выходе получается таблица квадратов от 10 до 15
            Console.WriteLine(String.Join(Environment.NewLine, result));
        }
```

---

# Задание 3 
Предыстория:

Для автоматизации формирования таблиц, которые будут выведены пользователю, необходимо унифицировать методы формирования таблицы.

Задание:

- Опишите класс колонки таблицы, который может иметь любую вложенность колонок и содержащий в себе поля: ключ, значение, признак является ли колонка заголовочной.

- Напишите метод, возвращающий все ключи колонок и вложенных колонок (вложенность не ограничена), по условию, переданному во входных параметрах. Условие не должно задаваться перечислением, предполагаем, что условие задает программист исходя из потребности (например, чтобы каждый раз не править исходный код метода). Поиск может осуществляться по ключу, значению и признаку.

```
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

            //Задание с получением списка ключей
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
```
