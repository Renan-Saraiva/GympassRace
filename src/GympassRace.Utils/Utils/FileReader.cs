using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GympassRace.Utils
{
    public class FileReader : IDisposable
    {
        private StreamReader _stream = null;
        public bool HeaderInFirstLine { get; set; }        
        public string Header { get; set; }
        public string Line { get; set; }


        public FileReader(Stream stream, bool headerInFirstLine = true)
        {
            HeaderInFirstLine = headerInFirstLine;
            _stream = new StreamReader(stream);
        }
        
        public void Open()
        {
            if (HeaderInFirstLine)
                Header = GetLine();
        }

        public void Close()
        {
            _stream.Close();
            _stream.Dispose();
            _stream = null;
        }

        public bool ReadLine()
        {
            Line = GetLine();
            return (Line != null);
        }

        private string GetLine()
        {
            string row = _stream.ReadLine();

            if (row == null)
                return null;

            return row.Replace("\t", string.Empty).Replace(" ", string.Empty);
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }
        }
    }
}
