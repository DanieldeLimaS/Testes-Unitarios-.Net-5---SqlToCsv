using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlOutputToCsv
{
    public class ConversorSqlQueryOutputParaCsv
    {
        public string Converter(List<string> linhasQueryOutput)
        {
            try
            {
                string texto = string.Empty;
                foreach (var item in linhasQueryOutput)
                {
                    if (item == "\r\n")
                        continue;
                    if (item.Contains("row(s) affected"))
                        continue;
                    if (item.Substring(0, 1) != "-")
                    {
                        var content = item.Split(",");
                        for (int i = 0; i < content.Count(); i++)
                        {
                            if (content[i].Trim() != "NULL")
                                texto += content[i].Trim();
                            if (i < content.Count() - 1)
                                texto += ",";
                            else texto += "\r\n";
                        }

                    }
                }

                return texto;
            }
            catch (NullReferenceException ex)
            {
                throw new NullReferenceException(ex.Message);
            }

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
