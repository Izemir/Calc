using System;
using System.Collections.Generic;


namespace Calc
{

    /// 
    /// Считает результат выражения
    /// 
    class Calculator
    {

        double result; // результат

        string errorMessage;  // сообщение ошибки

        bool error; // сообщение о том, есть ли ошибка

        public Calculator() {
            result = 0;
            error = false;
            errorMessage = "Pass";
                    }


        // Для подсчета используется Обратная польская запись, 
        // алгоритм записи и расчета подробно описан здесь https://ru.wikipedia.org/wiki/Обратная_польская_запись
        //
        public void calculate(List<object> datalist)
        {
            
            Stack<object> stack = new Stack<object>();
            List<object> tmpList = new List<object>();

            bool doubleBefore = false;

            // Преобразование выражения в обратную польскую запись
            for (int i = 0; i < datalist.Count; i++)
            {
                var temp = datalist[i];
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

            // Очистка стека, копирование стека в новое выражение
            while (stack.Count > 0)
            {
                tmpList.Add(stack.Pop());
            }

            // Расчет выражения, последнее оставшееся число в стеке и есть результат           
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

                    
                }
                else break;


                
            
            }


            result = Convert.ToDouble(stack.Pop());

        }

        // Возвращает сообщение ошибки
        public string getMessage() { return errorMessage; }

        // Возвращает значение переменной, описывающей, была ли ошибка
        public bool hasErrors() { return error; }

        // Возвращает результат расчета
        public string getResult()
        {
            return result.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        }

    }


}
