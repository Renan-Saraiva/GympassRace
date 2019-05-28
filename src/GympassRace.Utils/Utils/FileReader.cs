using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace GympassRace.Utils
{
    public class TextFileReader : IDisposable
    {
        StreamReader _stream = null;
        string _pattern;

        string _filePath;
        public string FilePath
        {
            get { return _filePath; }
        }

        string _fieldSeparator;
        public string FieldSeparator
        {
            get { return _fieldSeparator; }
            set { _fieldSeparator = value; }
        }

        string _textDelimiter;
        public string TextDelimiter
        {
            get { return _textDelimiter; }
            set { _textDelimiter = value; }
        }

        bool _headerInFirstLine;
        public bool HeaderInFirstLine
        {
            get { return _headerInFirstLine; }
            set { _headerInFirstLine = value; }
        }

        List<string> _header = null;
        public List<string> Header
        {
            get { return _header; }
            set { _header = value; }
        }

        List<string> _line = null;
        public List<string> Line
        {
            get { return _line; }
            set { _line = value; }
        }

        public TextFileReader(string filePath)
        {
            _filePath = filePath;
            _stream = new StreamReader(_filePath);
        }

        public TextFileReader(string filePath, string fieldSeparator, string textDelimiter, bool headerInFirstLine)
        {
            _fieldSeparator = fieldSeparator;
            _textDelimiter = textDelimiter;
            _headerInFirstLine = headerInFirstLine;
            _filePath = filePath;
            _stream = new StreamReader(_filePath);
        }

        public void Open()
        {
            _pattern = String.Format("({0}[^{2}]*{0})({1}{0}[^{2}]*{0})*", _textDelimiter, _fieldSeparator, (_textDelimiter.Length > 0 ? _textDelimiter : _fieldSeparator));
            if (_headerInFirstLine)
                _header = GetLine();
        }

        public void Close()
        {
            _stream.Close();
            _stream.Dispose();
            _stream = null;
        }

        public bool ReadLine()
        {
            _line = GetLine();
            return (_line != null);
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
                        if (cv.StartsWith(_fieldSeparator))
                            cv = cv.Remove(0, _fieldSeparator.Length);
                        if (_textDelimiter.Length > 0 && cv.StartsWith(_textDelimiter))
                            cv = cv.Remove(0, _textDelimiter.Length);
                        if (cv.EndsWith(_fieldSeparator))
                            cv = cv.Remove(cv.Length - _fieldSeparator.Length);
                        if (_textDelimiter.Length > 0 && cv.EndsWith(_textDelimiter))
                            cv = cv.Remove(cv.Length - _textDelimiter.Length);
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
