using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureML
{
    public class ConsoleFixedPositionMessage
    {
        private int _lineCount;
        private int _cursorTop;
        private ConsoleSpinner _spinner;

        public ConsoleFixedPositionMessage(int lineCount = 1, bool enableSpinner = false)
        {
            _lineCount = lineCount;
            _cursorTop = Console.CursorTop;
            if(enableSpinner)
            {
                _spinner = new ConsoleSpinner();
            }
        }

        public void WriteContent(string line, bool finalMessage = false)
        {
            WriteContent(new string[] { line }, finalMessage);
        }

        public void WriteContent(IReadOnlyList<string> lines, bool finalMessage = false)
        {
            if(lines.Count != _lineCount && !finalMessage)
            {
                throw new ArgumentException($"Expected {_lineCount} lines, but got {lines.Count}.");
            }

            ClearLines(_cursorTop, _lineCount);

            Console.CursorTop = _cursorTop;
            Console.CursorLeft = 0;

            bool wroteSpinner = false;

            foreach (var next in lines)
            {
                if (_spinner != null && !wroteSpinner && !finalMessage)
                {
                    Console.WriteLine(next + " " + _spinner.GetNextFrame());
                    wroteSpinner = true;
                }
                else
                {
                    Console.WriteLine(next);
                }
            }
        }

        public static void ClearLines(int cursorTop, int lineCount)
        {
            foreach(var next in Enumerable.Range(0, lineCount))
            {
                WriteScreenWideLine(' ', cursorTop + next);
            }
        }

        internal static void WriteScreenWideLine(char content, int cursorTop)
        {
            Console.CursorTop = cursorTop;
            Console.CursorLeft = 0;
            foreach (var next in Enumerable.Range(0, Console.BufferWidth))
            {
                Console.Write(content);
            }
        }
    }
}
