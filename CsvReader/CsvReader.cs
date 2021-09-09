using System;
using System.Collections.Generic;
using System.IO;

namespace CsvReader
{
    class CsvReader : IDisposable
    {
        private string[] _headers;
        private Dictionary<string, string> _row;
        private string _filePath;
        private string _seperator;
        private bool _containsHeaders;
        private int _rowCount;

        public CsvReader(string filePath, bool containsHeaders, string seperator)
        {
            FilePath = filePath;
            Seperator = seperator;
            ContainsHeaders = containsHeaders;
            RowCount = GetRowCount();
            if (containsHeaders)
            {
                Headers = GetHeaders();
            }
        }

        public string[] Headers
        {
            get { return _headers; }
            set { _headers = value; }
        }

        public Dictionary<string, string> Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string Seperator
        {
            get { return _seperator; }
            set { _seperator = value; }
        }

        public bool ContainsHeaders
        {
            get { return _containsHeaders; }
            set { _containsHeaders = value; }
        }

        public int RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }

        private string[] GetHeaders()
        {
            using (StreamReader rdr = new StreamReader(FilePath))
            {
                string line;
                while ((line = rdr.ReadLine()) != null && RowCount == 0)
                {
                    string[] columnHeaders = line.Split(Seperator);
                    Headers = new string[columnHeaders.Length];

                    for (int i = 0; i < columnHeaders.Length; i++)
                    {
                        Headers[i] = columnHeaders[i];
                    }
                }
            }

            return Headers;
        }

        public Dictionary<string, string> ReadLines()
        {
            Row = new Dictionary<string, string>();
            using (StreamReader rdr = new StreamReader(FilePath))
            {
                string line;
                while ((line = rdr.ReadLine()) != null && RowCount == 0)
                {
                    if (ContainsHeaders)
                    {
                        continue;
                    }

                    string[] columns = line.Split(Seperator);
                    if (Headers.Length != columns.Length)
                    {
                        throw new Exception("There is a column and header mismatch. Please validate file");
                    }

                    if (ContainsHeaders && Headers.Length == columns.Length)
                    {
                        for (int i = 0; i < columns.Length; i++)
                        {
                            //trim off double quotes from the start or end of the column as the csv could actually be seperated by "," instead of just ,
                            if (columns[i].StartsWith('\"'))
                            {
                                columns[i] = columns[i].Substring(1, columns[i].Length);
                            }
                            if (columns[i].EndsWith('\"'))
                            {
                                columns[i] = columns[i].Substring(0, columns[i].Length - 1);
                            }
                            Row.Add(Headers[i], columns[i]);
                        }
                    }
                    return Row;
                }
            }
            //returned last instance of Row
            return Row;

        }

        public int GetRowCount()
        {
            int count = 0;
            using (StreamReader rdr = new StreamReader(FilePath))
            {
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    if (ContainsHeaders && count == 0)
                    {
                        continue;
                    }

                    count++;
                }
            }

            return count;
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
