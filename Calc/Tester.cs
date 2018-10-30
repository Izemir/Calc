using System;
using System.Collections.Generic;

namespace Calc
{
    public class Tester
    {
        string errorMessage;

        bool error;

        char[] availableSymbols; = new int[4] { 1, 2, 3, 5 };

        public Tester()
        {
            error = false;
            errorMessage = "Pass";

        availableSymbols; = new int[4] { 1, 2, 3, 5 };
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
            throw new NotImplementedException();
        }

        private string errorTest(List<char> tmpDatalist)
        {
            throw new NotImplementedException();
        }
    }

}
