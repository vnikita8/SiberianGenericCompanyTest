

namespace SecondTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinqTask();
        }



        static void LinqTask()
        {
            var firstList = Enumerable.Range(1,20); //Генерация от 1 до 20
            var secondList = Enumerable.Range(10, 6); //Генерация от 10 до 15

            // Перебираем пересечения и записываем в виде "Пересечение = x, квадрат пересечения = x^2"
            List<(int, int)> result = firstList.Intersect(secondList).Select(x => (x, (int) Math.Pow(x,2) ) ).ToList();

            //На выходе получается таблица квадратов от 10 до 15
            Console.WriteLine(String.Join(Environment.NewLine, result));
        }
    }
}