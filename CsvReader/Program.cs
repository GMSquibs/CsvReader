using System;
using System.Globalization;

namespace CsvReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string _cmdResult = string.Empty;
            bool _validCmResult = false;
            bool _displayResults= false;
            Console.WriteLine("Enter the file path to the CSV you would like to read");

            string filePath = Console.ReadLine();
           
            while(!_validCmResult)
            {
                Console.WriteLine("Would you like to display the results. Enter Y for Yes or N for No, or press enter to exit.");

                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    break;
                }

                _cmdResult = Console.ReadLine().Trim(new char[] { '\"', '\'' }); //trim quotes if entered.

                if (_cmdResult.Length != 1 || string.Compare(_cmdResult,"Y", true, CultureInfo.InvariantCulture) == 0 || string.Compare(_cmdResult, "N", true, CultureInfo.InvariantCulture) == 0)
                {
                    Console.WriteLine("Im sorry, the command you entered is invalid. Please try again.");
                }
                else
                {
                    _validCmResult = true;
                    if (_cmdResult == "Y")
                    {
                        _displayResults = true;
                    }
                }
            }

            if (_displayResults)
            {
                using (CsvReader rdr = new CsvReader(filePath, true, ","))
                {
                    var hdrs = rdr.Headers;
                    string rowData = string.Empty;
                    foreach (var row in rdr.ReadLines())
                    {
                        var column = row.Key;
                        var columnValue = row.Value;

                        rowData += $"{column}:{columnValue},";
                    }

                    Console.WriteLine(rowData.TrimEnd(','));
                }
            }

            Console.ReadKey();
            
        }
    }
}
