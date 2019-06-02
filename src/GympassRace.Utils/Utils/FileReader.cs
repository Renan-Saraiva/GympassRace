using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        
        public async Task OpenAsync()
        {
            if (HeaderInFirstLine)
                Header = await GetLineAsync();
        }

        public void Close()
        {
            _stream.Close();
            _stream.Dispose();
            _stream = null;
        }

        public async Task<bool> ReadLineAsync()
        {
            Line = await GetLineAsync();
            return (Line != null);
        }

        private async Task<string> GetLineAsync()
        {
            string row = await _stream.ReadLineAsync();

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
