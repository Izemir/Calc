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

        static string answer = "@";

        static int errorNumber = 0;


        static void Main(string[] args)
        {
            
            //Testing();

            List<object>  a = Parsing();

            /*
            for (int i = 0; i < a.Count; i++)
            {
                Console.Write(a[i]);
            }
            */
            
            

            answer = Calculate(a);


            Console.WriteLine(answer);
            Console.ReadKey();
        }


        private static void Testing()
        {
            List<char> datalist = new List<char>();
            datalist.AddRange(input);

            //TestForBrackets(datalist);

            //TestForErrors(datalist);
        }

        private static string TestForErrors(List<char> datalist)
        {
            /*

             проверяем /0 или / 0
             проверяем на числа
             и в том же цикле проверяем на +-.. и ( и .

             как то встроить проверку на числа подряд и знаки подряд(и исключение унарный минус)

             */

            for (int i = 0; i < datalist.Count; i++)
            {
                char c = datalist[i];
                if (c != ' ' && c != '(' && c != ')' && char.IsDigit(c) && c!='+' && c!='-' && c!= '–' && c!= '*' && c != '/' && c != '.')
                {
                    
                    return "Ошибка: " + c.ToString() + " на позиции " + i;
                }
            }


            Dictionary<int, char> tmpMap = new Dictionary<int, char>();

            for(int i=0;i<datalist.Count;i++)
            {
                char c = datalist[i];

                if (c != ' ' && c != '(' && c != ')') tmpMap.Add(i, c);
            }



            return "Pass";
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

        private static List<object> Parsing()
        {

            List<char> datalist = new List<char>();
            List<char> tmp = new List<char>();
            tmp.AddRange(input);


            foreach (char c in tmp)
            {
                if (c != ' ') datalist.Add(c);
            }

            List<object> result = new List<object>();

            bool readingNumber = false;

            List<char> number = new List<char>();

            for (int i = 0; i < datalist.Count; i++)
            {

                //if (datalist[i] == ' ') continue;

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

                    if (i + 1 == datalist.Count)
                    {
                        result.Add(Convert.ToDouble(new string(number.ToArray()), System.Globalization.CultureInfo.InvariantCulture));
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
                        if (datalist[i] == '.')
                        {
                            number.Add(datalist[i]);
                        }
                        else
                        {
                            result.Add(Convert.ToDouble(new string(number.ToArray()), System.Globalization.CultureInfo.InvariantCulture));
                            number.Clear();
                            readingNumber = false;
                            result.Add(datalist[i].ToString());
                        }
                    }

                }
            }


            return result;
        }


        //
        //
        //
        //использовать queue
        //
        private static string Calculate(List<object> a)
        {
            string result="";

            Stack<object> stack = new Stack<object>();
            List<object> tmpList = new List<object>();

            bool doubleBefore = false;

            for(int i = 0; i < a.Count; i++)
            {
                var temp = a[i];
                if (temp.GetType() == typeof(double))
                {
                    tmpList.Add(temp);
                    doubleBefore = true;
                }
                else if (string.Equals(temp, "("))
                {
                    stack.Push(temp);
                    doubleBefore = false;
                }
                else if (string.Equals(temp, ")"))
                {

                    while (stack.Count > 0)
                    {
                        if (!string.Equals(stack.Peek(), "("))
                        {
                            tmpList.Add(stack.Pop());
                        }
                        else
                        {
                            stack.Pop();
                            break;
                        }
                    }
                    doubleBefore = false;
                }
                else if((string.Equals(temp, "-") || string.Equals(temp, "–")) && !doubleBefore)
                {
                    temp = "$";
                    stack.Push(temp);
                    
                    doubleBefore = false;

                }
                else if (string.Equals(temp, "+") || string.Equals(temp, "-") || string.Equals(temp, "–"))
                {
                    
                    if (stack.Count == 0) stack.Push(temp);
                    else
                    {
                        while (stack.Count > 0)
                        {
                            var temp2 = stack.Peek();

                            if (string.Equals(temp2, "+") || string.Equals(temp2, "-") || string.Equals(temp, "–") || string.Equals(temp2, "*") || string.Equals(temp2, "/"))
                            {
                                tmpList.Add(stack.Pop());
                                if (stack.Count == 0)
                                {
                                    stack.Push(temp);
                                    break;
                                }
                            }
                            else
                            {
                                stack.Push(temp);
                                break;
                            }
                            
                        }
                    }
                    doubleBefore = false;
                }
                else if (string.Equals(temp, "*") || string.Equals(temp, "/"))
                {
                    if (stack.Count == 0) stack.Push(temp);
                    else
                    {
                        while (stack.Count > 0)
                        {
                            var temp2 = stack.Peek();

                            if (string.Equals(temp2, "*") || string.Equals(temp2, "/"))
                            {
                                tmpList.Add(stack.Pop());
                                if (stack.Count == 0)
                                {
                                    stack.Push(temp);
                                    break;
                                }
                            }
                            else
                            {
                                stack.Push(temp);
                                break;
                            }
                            
                        }
                    }
                    doubleBefore = false;
                }



            }

            while (stack.Count > 0)
            {
                tmpList.Add(stack.Pop());
            }



            foreach (var temp in tmpList)
            {
                if (temp.GetType() == typeof(double))
                {
                    stack.Push(temp);
                    
                }
                else
                {
                    if (string.Equals(temp, "$")) {
                        
                        double second = Convert.ToDouble(stack.Pop());
                        stack.Push(-1 * second);
                    }
                    else {

                        double second = Convert.ToDouble(stack.Pop());
                        double first = Convert.ToDouble(stack.Pop());

                        double res = 0;

                        switch (temp)
                        {
                            case "+":
                                res = first + second;
                                break;
                            case "-":
                                res = first - second;
                                break;
                            case "–":
                                res = first - second;
                                break;
                            case "*":
                                res = first * second;
                                break;
                            case "/":
                                res = first / second;
                                break;


                        }

                        stack.Push(res);
                    }
                }

            }



            return Convert.ToDouble(stack.Pop()).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture); 
        }

        

        public static bool IsCharDigit(char c)
        {
            return ((c >= '0') && (c <= '9'));
        }
    }

}
