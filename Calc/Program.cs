using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {



       

        public static string input= "85 / 10 – 4 * (10 - 55)";


        static char[] parsing;

        static string answer = "@";

        static int errorNumber = 0;


        static void Main(string[] args)
        {



            //Testing();

            List<string>  a = Parsing();

            for (int i = 0; i < a.Count; i++)
            {
                Console.WriteLine(i + " - " + a[i]);
            }

            answer = Calculate2(a);

            Console.WriteLine(answer);
            Console.ReadKey();
        }

        private static string Calculate2(List<string> a)
        {
            Stack<string> stack = new Stack<string>();


        }


        //ДОБАВИТЬ ТОЧКУ
        private static List<string> Parsing()
        {

            List<char> datalist = new List<char>();
            datalist.AddRange(input);

            List<string> result = new List<string>();

            bool readingNumber = false;

            List<char> number = new List<char>();

            for (int i = 0; i < datalist.Count; i++)
            {

                if (datalist[i] == ' ') continue;

                if (char.IsDigit(datalist[i]))
                {
                    
                    if (!readingNumber)
                    {
                        readingNumber = true;
                        number.Add(datalist[i]);
                    }
                    else
                    {
                        number.Add(datalist[i]);
                    }

                    if(i+1== datalist.Count)
                    {
                        result.Add(new string(number.ToArray()));
                        number.Clear();
                        readingNumber = false;
                    }


                }
                else
                {
                    if (!readingNumber)
                    {
                        result.Add(datalist[i].ToString());
                    }
                    else
                    {

                        result.Add(new string(number.ToArray()));
                        number.Clear();
                        readingNumber = false;
                        result.Add(datalist[i].ToString());
                    }

                }
            }


            return result;
        }

        private static void Testing()
        {
            List<char> datalist = new List<char>();
            datalist.AddRange(input);

            //TestForBrackets(datalist);

            //TestForErrors(datalist);
        }

        private static void TestForErrors(List<char> datalist)
        {
           /*
           
            проверяем /0 или / 0
            проверяем на числа
            и в том же цикле проверяем на +-.. и ( и .
            
            как то встроить проверку на числа подряд и знаки подряд(и исключение унарный минус)


            */
        }

        private static void TestForBrackets(List<char> datalist)
        {
            

            List<char> brackets = new List<char>();

            /*
             * 
             * Идем циклом через выражение, сохраняем индекс ( в стек
             * встречаем ) и меняем ( и ) на нули
             * и удаляем индекс ( из стека
             * 
             * изначально индекс -1
             * 
             * считаем кол-во ( и )
             * 
             */
           


        }

        public static void Calculate()
        {
            // массив из инпута
            parsing = input.ToCharArray();

            //List<string> datalist = new List<string>();
            //datalist.AddRange(input.Select(c => c.ToString()));

            //лист из инпута
            List<char> datalist = new List<char>();
            datalist.AddRange(input);



            answer = ParseAndCalc(datalist);
            //answer.text = datalist.Count.ToString();
            //answer.text = ParseAndCalc(parsing);
        }

        public static string ParseAndCalc(List<char> toParse)
        {
            //List<char> result = new List<char>();

            //string b = "@!";

            char operation = '\0';
            List<char> first = new List<char>();
            List<char> second = new List<char>();

            while (toParse.Count > 0 && operation!='e')
            {
                

                if (char.IsDigit(toParse[0])||toParse[0]=='.')
                {
                    if (operation == '\0')
                    {
                        first.Add(toParse[0]);
                        toParse.RemoveAt(0);
                    }
                    else
                    {
                        if (operation != 'e')
                        {
                            second.Add(toParse[0]);
                            toParse.RemoveAt(0);
                        }
                    }
                    
                }
                else
                {
                    if ((toParse[0] == '+') || (toParse[0] == '-') || (toParse[0] == '*') || (toParse[0] == '/'))
                    {
                        operation = toParse[0];
                        toParse.RemoveAt(0);
                    }
                    else if(toParse[0] == ' ')
                    {
                        toParse.RemoveAt(0);
                    }
                    else
                    {
                        operation = 'e'; break;
                    }
                }



            }

            if (operation == 'e') return "Error";
            else return Calculator(first, second, operation);



            /*
            for (int i = 0; i < toParse.Count; i++)
            {
                if (toParse.Count != 0)
                {
                    if (char.IsDigit(toParse[i]))
                    {
                        result.Add(toParse[i]);
                        toParse.RemoveAt(i);
                        i--;
                    }
                    else
                    {

                        toParse.RemoveAt(i);
                        b = ParseAndCalc(toParse);
                        break;
                    }
                }


            }


            string a = new string(result.ToArray());
            //b = new string(toParse.ToArray());
            */

            //return "S:" + a + ";Con:" + b;
        }

        private static string Calculator(List<char> first, List<char> second, char operation)
        {
                       
            double a = Convert.ToDouble(new string (first.ToArray()), System.Globalization.CultureInfo.InvariantCulture);
            double b = Convert.ToDouble(new string(second.ToArray()), System.Globalization.CultureInfo.InvariantCulture);

            double res=0.0;

            switch (operation)
            {
                case '+':
                    res = a + b;
                    break;
                case '-':
                    res = a - b;
                    break;
                case '*':
                    res = a * b;
                    break;
                case '/':
                    res = a / b;
                    break;

            }

            return res.ToString("0.000000", System.Globalization.CultureInfo.InvariantCulture); ;
        }

        public static bool IsCharDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }
    }

}
