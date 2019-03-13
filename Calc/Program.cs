using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        
        public static string input= "-2+3*2";

        static string answer = "@";

        static bool error = false;
        


        static void Main(string[] args)
        {

            List<char> datalist = new List<char>();
            datalist.AddRange(input);

            Tester test = new Tester();

            Parser parser = new Parser();

            Calculator calculator = new Calculator();

            test.startTest(datalist);

            //string errorMessage = Testing();

            if (test.hasErrors())
            {
                Console.WriteLine(test.getMessage());
                Console.ReadKey();
            }
            else
            {


                //List<object> a = Parsing();

                parser.Parse(datalist);

                List<object> a = parser.getParsedList();

                /*
                for (int i = 0; i < a.Count; i++)
                {
                    Console.Write(a[i]);
                }
                */

                calculator.calculate(a);

                //answer = Calculate(a);

                if(calculator.hasErrors())
                {
                    Console.WriteLine(calculator.getMessage());
                    Console.ReadKey();
                }
                else
                {
                    //Console.WriteLine(answer);
                    Console.WriteLine(calculator.getResult());
                    Console.ReadKey();
                }
                

            }

            
        }

        //тест на точки
        //тест на ax
        //тест на ( - номер скобочки в ошибке
        private static string Testing()
        {
            List<char> datalist = new List<char>();
            datalist.AddRange(input);

            string result;

            result = TestForBrackets(datalist);

            datalist.Clear();
            datalist.AddRange(input);

            if (!error) result = TestForErrors(datalist);

            return result;
        }

        private static string TestForErrors(List<char> testData)
        {
            /*

             проверяем /0 или / 0
             проверяем на числа
             и в том же цикле проверяем на +-.. и ( и .

             как то встроить проверку на числа подряд и знаки подряд(и исключение унарный минус)

             проверка . на числа вокруг
             */

            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if (c != ' ' && c != '(' && c != ')' && !char.IsDigit(c) && c!='+' && c!='-' && c!= '–' && c!= '*' && c != '/' && c != '.')
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i+1)+" (неверный символ)";
                }
            }

            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if (c == '.')
                {
                    if(i==0 || i + 1 == testData.Count)
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                    }
                    else if(!char.IsDigit(testData[i-1])|| !char.IsDigit(testData[i+1]))
                        {
                            error = true;
                            return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                        }
                    
                    
                }

            }



            char symbolBefore='?';
            int lastNumberIndex = -1;
            

            //
            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if(c == '+' || c == '*' || c == '/')
                {
                    if (symbolBefore == '+')
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (несколько знаков операций подряд)";
                    }
                    else
                    {
                        symbolBefore = '+';
                    }
                }
                else if(c == '-' || c == '–')
                {
                    symbolBefore = '+';
                }
                else if (char.IsDigit(c)||c=='.')
                {
                    if (symbolBefore == '1'&& lastNumberIndex <i-1)
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (несколько чисел подряд)";
                    }
                    else
                    {
                        lastNumberIndex = i;
                        symbolBefore = '1';
                    }
                }
                


            }

            lastNumberIndex = -1;
            bool operationWithoutSecondNumber = false;
            int operationIndex = -1;
            char operation = '?';

            //знак без числа впереди и после
            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if (char.IsDigit(c))
                {
                    operationWithoutSecondNumber = false;
                    lastNumberIndex = i;
                }
                
                else if ((c == '+' || c == '*' || c == '/')&&lastNumberIndex==-1)
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование знака операции)";
                }
                else if(c == '+' || c == '*' || c == '/' || c == '-' || c == '–')
                {
                    operationWithoutSecondNumber = true;
                    operationIndex = i;
                    operation = c;
                }


            }

            if (operationWithoutSecondNumber)
            {
                error = true;
                return "Ошибка: " + operation.ToString() + " на позиции " + (operationIndex + 1) + " (неверное использование знака операции)";
            }

            



                return "Pass";
        }

        private static string TestForBrackets(List<char> testData)
        {

            List<char> testData2 = testData.GetRange(0, testData.Count);
            
            Stack<int> bracketIndex = new Stack<int>();

            for(int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if (c == '(')
                {
                    bracketIndex.Push(i);
                }
                else if (c == ')')
                {
                    if (bracketIndex.Count == 0)
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование скобок)";
                    }
                    else
                    {
                        testData[bracketIndex.Pop()] = '0';
                        testData[i] = '0';
                    }
                }
            }

            if (testData.Contains('(') || testData.Contains(')'))
            {
                for (int i = 0; i < testData.Count; i++)
                {
                    char c = testData[i];
                    if (c == '(' || c == ')')
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование скобок)";
                    }
                }
            }

            bool bracketBefore = false;
            bool operBefore = false;
            int index = -1;


            for (int i = 0; i < testData2.Count; i++)
            {
                char c = testData2[i];

                if (c == '(')
                {
                    bracketBefore = true;
                    index = i;
                }
                else if((c == '+' || c == '*' || c == '/')&& bracketBefore)
                {
                        error = true;
                        return "Ошибка: " + "(" + " на позиции " + (index + 1) + " (неверное использование скобок)";
                                       
                }
                else if(c == '+' || c == '*' || c == '/' || c == '-' || c == '–')
                {
                    operBefore = true;
                }
                else if (char.IsDigit(c))
                {
                    bracketBefore = false;
                }
                else if((c==')' && bracketBefore)|| (c == ')' && operBefore))
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование скобок)";
                }




            }
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



                return "Pass";
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

                                if (second != 0)
                                {
                                    res = first / second;
                                    break;
                                }
                                else
                                {
                                    error = true;
                                    return "Предотвращено деление на ноль";
                                    
                                }

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
