using System;
using System.Collections.Generic;


namespace Calc
{
    class Calculator
    {

        double result;

        string errorMessage;

        bool error;

        public Calculator() {
            result = 0;
            error = false;
            errorMessage = "Pass";
                    }

        
        public void calculate(List<object> a)
        {
            

            Stack<object> stack = new Stack<object>();
            List<object> tmpList = new List<object>();

            bool doubleBefore = false;

            for (int i = 0; i < a.Count; i++)
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

                }
                else if ((string.Equals(temp, "-") || string.Equals(temp, "–")) && !doubleBefore)
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

                            if (string.Equals(temp2, "+") || string.Equals(temp2, "-") || string.Equals(temp, "–") || string.Equals(temp2, "*") || string.Equals(temp2, "/") || string.Equals(temp2, "$"))
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

                            if (string.Equals(temp2, "*") || string.Equals(temp2, "/") || string.Equals(temp2, "$"))
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


            /*
            foreach (var temp in tmpList)
            {
                Console.Write(temp.ToString()+",");
                
            }
            Console.ReadKey();
            */

            foreach (var temp in tmpList)
            {
                if (!error)
                {
                    if (temp.GetType() == typeof(double))
                    {
                        stack.Push(temp);

                    }
                    else
                    {
                        if (string.Equals(temp, "$"))
                        {

                            double second = Convert.ToDouble(stack.Pop());
                            stack.Push(-1 * second);
                        }
                        else
                        {

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

                                    if (second != 0)
                                    {
                                        res = first / second;
                                        break;
                                    }
                                    else
                                    {
                                        error = true;
                                        errorMessage = "Предотвращено деление на ноль";
                                        break;
                                    }

                            }

                            stack.Push(res);
                        }
                    }

                    result = Convert.ToDouble(stack.Pop());
                }
                else break;


                
            
            }

        }

        public string getMessage() { return errorMessage; }

        public bool hasErrors() { return error; }

        public string getResult()
        {
            return result.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

    }


}
