using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Parser
    {
        List<object> parsedList;

        public Parser() { }

        public void Parse(List<char> datalist)
        {
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

            parsedList= result;
        }


        public List<object> getParsedList()
        {
            return parsedList;
        }
    }

    
}
