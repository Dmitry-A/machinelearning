namespace AzureML
{
    internal class ConsoleSpinner
    {
        private char[] _spinnerFilmStrip;
        private int _nextFrame;

        public ConsoleSpinner()
        {
            _spinnerFilmStrip = new char[] { '|', '/', '-', '\\' };
        }

        public char GetNextFrame()
        {
            return _spinnerFilmStrip[_nextFrame++ % (_spinnerFilmStrip.Length - 1)];
        }
    }
}
