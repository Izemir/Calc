using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{

    /// 
    /// Распознает числа, переводит выражение в более удобное для последующих расчетов
    ///
    class Parser
    {
        List<object> parsedList; // результат парсинга

        public Parser() { }

        public void parse(List<char> datalist)
        {
            List<object> result = new List<object>(); // полученное выражение

            bool readingNumber = false; // определяет, идет ли в данный момент чтение цифр

            List<char> number = new List<char>(); // лист цифр, позднее объединямых в число


            /* Проходит через входную строку, цифры, идущие подряд, объединяет в числа,
             * также распознает числа с точкой.
             */
            for (int i = 0; i < datalist.Count; i++)
            {
                                
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

            parsedList= result;
        }

        // Возвращает результат парсинга
        public List<object> getParsedList()
        {
            return parsedList;
        }
    }

    
}
