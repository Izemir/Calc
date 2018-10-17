using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {


        public static string input="112/33";

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
            List<char> result = new List<char>();

            string b = "@!";


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
            

            return "S:" + a + ";Con:" + b;
        }

        public static bool IsCharDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }
    }

}
