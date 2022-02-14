using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SRTM.Sources.CGIAR
{
    /// <summary>
    /// An Esri grid is a raster GIS file format developed by Esri, which has two formats:
    /// 
    /// A proprietary binary format, also known as an ARC/INFO GRID, ARC GRID and many other variations
    /// A non-proprietary ASCII format, also known as an ARC/INFO ASCII GRID
    /// The formats were introduced for ARC/INFO. The binary format is widely used within Esri programs, such /// as ArcGIS, while the ASCII format is used as an exchange, or export format, due to the simple and portable ASCII file structure.
    /// 
    /// The grid defines geographic space as an array of equally sized square grid points arranged in rows and /// columns. Each grid point stores a numeric value that represents a geographic attribute (such as elevation or surface slope) for that unit of space. Each grid cell is referenced by its x,y coordinate location.
    /// https://en.wikipedia.org/wiki/Esri_grid
    /// Spec: http://help.arcgis.com/en/arcgisdesktop/10.0/help/index.html#//009t0000000w000000
    /// </summary>
    public class ASCIIGridFile
    {
        private FileStream _fileStream;
        private StreamReader _streamReader;
        private readonly string _filename;
        private static char[] SEPARATOR = new char[] { ' ' };

        List<List<string>> _data = null;
        private static Dictionary<string, List<List<string>>> _tempCache = new Dictionary<string, List<List<string>>>();

        public ASCIIGridFile(string fileName)
        {
            this._filename = fileName;
            _fileStream = new FileStream(_filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            _streamReader = new StreamReader(_fileStream, Encoding.ASCII);
        }
        public float GetElevationAtPoint(FileMetadata metadata, int x, int y)
        {
            if (_data == null)
            {
                ReadAllFile(metadata);
            }

            string strXValue = _data[y][x];

            float elevation = float.Parse(strXValue, CultureInfo.InvariantCulture);
            return elevation;

        }

        private void ReadAllFile(FileMetadata metadata)
        {
            if (_tempCache.ContainsKey(_filename))
            {
                _data = _tempCache[_filename];
                return;
            }
            string curLine = null;
            _fileStream.Seek(0, SeekOrigin.Begin);

            // skip header
            for (int i = 1; i <= 6 /* + (y - 1)*/; i++)
            {
                curLine = _streamReader.ReadLine();
            }

            _data = new List<List<string>>(metadata.Height);
            while (!_streamReader.EndOfStream)
            {
                var line = _streamReader.ReadLine().Trim();

                var values = new List<string>(metadata.Width);
                var current = string.Empty;
                foreach (char c in line)
                {
                    if (c == ' ')
                    {
                        values.Add(current);
                        current = string.Empty;
                    }
                    else
                    {
                        current += c;
                    }
                }
                values.Add(current);
                _data.Add(values);
            }
            _tempCache[_filename] = _data;
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _streamReader?.Dispose();
                    _fileStream?.Dispose();
                }

                disposedValue = true;
            }
        }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            Dispose(true);
        }


        #endregion
    }
}
