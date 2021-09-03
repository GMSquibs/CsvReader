using System;

namespace CsvReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Test.csv";
            using (CsvReader rdr = new CsvReader(filePath,true, ","))
            {
                var hdrs = rdr.Headers;
                string rowData = string.Empty;
                foreach (var row in rdr.ReadLines())
                {
                    var column = row.Key;
                    var columnValue = row.Value;

                    rowData += $"{column},{columnValue}";
                }
            }
        }
    }
}
