using System;
using System.Collections.Generic;

namespace Calc
{



    /// 
    /// Тестирует введенную строку на разрешенные знаки и верное их использование
    /// 
    public class Tester
    {
        string errorMessage;  //сообщение ошибки

        bool error; //сообщение о том, есть ли ошибка
               

        public Tester()
        {
            error = false;
            errorMessage = "Pass";

        
    }

        // Возвращает сообщение ошибки
        public string getMessage() { return errorMessage; }

        // Возвращает значение переменной, описывающей, была ли ошибка
        public bool hasErrors() { return error; }

        /* Запускает несколько тестов:
        *   тест на пустое выражение
        *   тест на разрешенные символы;
        *   тест на использование скобок;
        *   тест на использование разрешенных символов.      
        */
        public void startTest(List<char> datalist)
        {


            List<char> tmpDatalist = new List<char>();

            tmpDatalist.AddRange(datalist);

            errorMessage = emptyTest(tmpDatalist);

            if (!error) errorMessage = basicTest(tmpDatalist);

            if (!error) errorMessage = bracketsTest(tmpDatalist);

            if (!error)
            {

                tmpDatalist.Clear();
                tmpDatalist.AddRange(datalist);

                errorMessage = errorTest(tmpDatalist);

            }
        }

        // Тест на пустую входную строку
        private string emptyTest(List<char> datalist)
        {
            List<char> testData = datalist.GetRange(0, datalist.Count);


            testData.RemoveAll(spaceChar);

            if (testData.Count == 0)
            {
                error = true;
                return "Ошибка: пустая входная строка.";
            }

            

            return "Pass";
        }

        // Предикат для теста на пустое выражение
        private bool spaceChar(char c)
        {
            return c == ' ';
        }

        // Тест на разрешенные символы (скобки, знаки операций, цифры,пробелы и точки)
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


        // Тест на верное использование скобок
        private string bracketsTest(List<char> datalist)
        {
            List<char> testData = datalist.GetRange(0, datalist.Count);

            Stack<int> bracketIndex = new Stack<int>();


            // Проходит через выражение, запоминает все '(',
            // если встречает ')', то удаляет из стека последнюю запись о '('
            // Также заменяет пары '(' и ')' на нули
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

            // Проверяет, остались ли после первого теста скобки в выражении 
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

            bool bracketBefore = false; // была ли скобка до [текущего символа]
            bool operBefore = false; // была ли операция до [текущего символа]
            int index = -1;

            testData.Clear();
            testData = datalist.GetRange(0, datalist.Count);


            // Проверяет на ошибочное использование скобок, например 2(+2)
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

        // Проверка на верное использование разрешенных знаков
        private string errorTest(List<char> datalist)
        {
            // использование точки, определяется, стоят ли рядом цифры
            for (int i = 0; i < datalist.Count; i++)
            {
                char c = datalist[i];
                if (c == '.')
                {
                    if (i == 0 || i + 1 == datalist.Count)
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                    }
                    else if (!char.IsDigit(datalist[i - 1]) || !char.IsDigit(datalist[i + 1]))
                    {
                        error = true;
                        return "Ошибка: " + c.ToString() + " на позиции " + (i + 1) + " (неверное использование точки)";
                    }


                }

            }



            char symbolBefore = '?';
            int lastNumberIndex = -1;


            // использование знаков операции или чисел подряд
            for (int i = 0; i < datalist.Count; i++)
            {
                char c = datalist[i];
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

            // определяется, есть ли числа до и после знака
            for (int i = 0; i < datalist.Count; i++)
            {
                char c = datalist[i];
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
