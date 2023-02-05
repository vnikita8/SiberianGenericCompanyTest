using System.Runtime.ConstrainedExecution;

namespace FirstTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(getMissing(new string[] {"O", "Q", "R", "S"}));
            TryGetMissing(new string[] { "O", "Q", "S" }, out string out_string);
            Console.WriteLine(out_string);

        }

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

    }
}