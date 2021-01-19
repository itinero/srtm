using System;

namespace SRTM.Sources
{
    internal sealed class FuncSource : ISRTMSource
    {
        private readonly Func<(string path, string name), bool> _getMissingCell;

        public FuncSource(Func<(string path, string name), bool> getMissingCell)
        {
            _getMissingCell = getMissingCell;
        }
        
        public bool GetMissingCell(string path, string name)
        {
            return _getMissingCell((path, name));
        }
    }
}