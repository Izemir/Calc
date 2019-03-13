using System;
using System.Collections.Generic;

namespace Calc
{
    public class Tester
    {
        string errorMessage;

        bool error;

        

        public Tester()
        {
            error = false;
            errorMessage = "Pass";

        
    }


        public string getMessage() { return errorMessage; }

        public bool hasErrors() { return error; }

        public void startTest(List<char> datalist)
        {
            List<char> tmpDatalist = new List<char>();

            tmpDatalist.AddRange(datalist);

            errorMessage = basicTest(tmpDatalist);

            if (!error) errorMessage = bracketsTest(tmpDatalist);

            if (!error)
            {

                tmpDatalist.Clear();
                tmpDatalist.AddRange(datalist);

                errorMessage = errorTest(tmpDatalist);

            }
        }

        private string basicTest(List<char> testData)
        {
            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];
                if (c != ' ' && c != '(' && c != ')' && !char.IsDigit(c) && c != '+' && c != '-' && c != '–' && c != '*' && c != '/' && c != '.')
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверный символ)";
                }
            }

            return "Pass";
        }

        private string bracketsTest(List<char> tmpDatalist)
        {
            List<char> testData = tmpDatalist.GetRange(0, tmpDatalist.Count);

            Stack<int> bracketIndex = new Stack<int>();

            for (int i = 0; i < testData.Count; i++)
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

            testData.Clear();
            testData = tmpDatalist.GetRange(0, tmpDatalist.Count);


            for (int i = 0; i < testData.Count; i++)
            {
                char c = testData[i];

                if (c == '(')
                {
                    bracketBefore = true;
                    index = i;
                }
                else if ((c == '+' || c == '*' || c == '/') && bracketBefore)
                {
                    error = true;
                    return "Ошибка: " + "(" + " на позиции " + (index + 1) + " (неверное использование скобок)";

                }
                else if (c == '+' || c == '*' || c == '/' || c == '-' || c == '–')
                {
                    operBefore = true;
                }
                else if (char.IsDigit(c))
                {
                    bracketBefore = false;
                }
                else if ((c == ')' && bracketBefore) || (c == ')' && operBefore))
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование скобок)";
                }




            }
           
            return "Pass";
        }

        private string errorTest(List<char> tmpDatalist)
        {
            /*

             проверяем /0 или / 0
             проверяем на числа
             и в том же цикле проверяем на +-.. и ( и .

             как то встроить проверку на числа подряд и знаки подряд(и исключение унарный минус)

             проверка . на числа вокруг
             */

            for (int i = 0; i < tmpDatalist.Count; i++)
            {
                char c = tmpDatalist[i];
                if (c != ' ' && c != '(' && c != ')' && !char.IsDigit(c) && c != '+' && c != '-' && c != '–' && c != '*' && c != '/' && c != '.')
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверный символ)";
                }
            }

            for (int i = 0; i < tmpDatalist.Count; i++)
            {
                char c = tmpDatalist[i];
                if (c == '.')
                {
                    if (i == 0 || i + 1 == tmpDatalist.Count)
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                    }
                    else if (!char.IsDigit(tmpDatalist[i - 1]) || !char.IsDigit(tmpDatalist[i + 1]))
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                    }


                }

            }



            char symbolBefore = '?';
            int lastNumberIndex = -1;


            //
            for (int i = 0; i < tmpDatalist.Count; i++)
            {
                char c = tmpDatalist[i];
                if (c == '+' || c == '*' || c == '/')
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
                else if (c == '-' || c == '–')
                {
                    symbolBefore = '+';
                }
                else if (char.IsDigit(c) || c == '.')
                {
                    if (symbolBefore == '1' && lastNumberIndex < i - 1)
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
            for (int i = 0; i < tmpDatalist.Count; i++)
            {
                char c = tmpDatalist[i];
                if (char.IsDigit(c))
                {
                    operationWithoutSecondNumber = false;
                    lastNumberIndex = i;
                }

                else if ((c == '+' || c == '*' || c == '/') && lastNumberIndex == -1)
                {
                    error = true;
                    return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование знака операции)";
                }
                else if (c == '+' || c == '*' || c == '/' || c == '-' || c == '–')
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
    }

}
