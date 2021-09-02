using System;

namespace CsvReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Test";
            using (CsvReader rdr = new CsvReader(filePath,true, ","))
            {
                var hdrs = rdr.Headers;
                foreach (var row in rdr.Read())
                {
                    var column = row.Key;
                    var columnValue = row.Value;
                }
            }
        }
    }
}
