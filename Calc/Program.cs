using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {


<<<<<<< HEAD
        public static string input="112.4 / 17.1";
=======
        public static string input="112/33";
>>>>>>> 06744f50f9e6bb16f069fe98b36832511bab5ca1

        static char[] parsing;

        static string answer = "@";


        static void Main(string[] args)
        {

            Calculate();

            Console.WriteLine(answer);
            Console.ReadKey();
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
