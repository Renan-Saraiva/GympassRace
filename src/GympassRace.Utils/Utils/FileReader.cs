using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GympassRace.Utils
{
    public class FileReader : IDisposable
    {
        private StreamReader _stream = null;
        private string _pattern;

        public string FieldSeparator { get; set; }
        public string TextDelimiter { get; set; }   
        public bool HeaderInFirstLine { get; set; }
        public List<string> Header { get; set; }
        public List<string> Line { get; set; }


        public FileReader(Stream stream)
        {
            _stream = new StreamReader(stream);
        }

        public FileReader(Stream stream, string fieldSeparator, string textDelimiter, bool headerInFirstLine = true)
        {
            FieldSeparator = fieldSeparator;
            TextDelimiter = textDelimiter;
            HeaderInFirstLine = headerInFirstLine;
            _stream = new StreamReader(stream);
        }

        public void Open()
        {
            _pattern = String.Format("({0}[^{2}]*{0})({1}{0}[^{2}]*{0})*", TextDelimiter, FieldSeparator, (TextDelimiter.Length > 0 ? TextDelimiter : FieldSeparator));
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

        private List<string> GetLine()
        {
            string row = _stream.ReadLine();
            if (row == null)
                return null;

            List<string> r = new List<string>();
            Match match = null;

            if (Regex.IsMatch(row, _pattern))
            {
                match = Regex.Match(row, _pattern);
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    foreach (Capture c in match.Groups[i].Captures)
                    {
                        string cv = c.Value;
                        if (cv.StartsWith(FieldSeparator))
                            cv = cv.Remove(0, FieldSeparator.Length);
                        if (TextDelimiter.Length > 0 && cv.StartsWith(TextDelimiter))
                            cv = cv.Remove(0, TextDelimiter.Length);
                        if (cv.EndsWith(FieldSeparator))
                            cv = cv.Remove(cv.Length - FieldSeparator.Length);
                        if (TextDelimiter.Length > 0 && cv.EndsWith(TextDelimiter))
                            cv = cv.Remove(cv.Length - TextDelimiter.Length);
                        r.Add(cv);
                    }
                }
            }
            return r;
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
