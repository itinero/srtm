// The MIT License (MIT)

// Copyright (c) 2017 Alpine Chough Software, Ben Abelshausen

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using SRTM.Logging;

namespace SRTM.Sources.CGIAR
{
    /// <summary>
    /// Defines an CGIAR source of data for SRTMv4.
    /// </summary>
    public class CGIARSource : ISRTMSource
    {
        /// <summary>
        /// The source of the data.
        /// </summary>
        public const string SOURCE = @"http://srtm.csi.cgiar.org/wp-content/uploads/files/srtm_5x5/ASCII";

        /// <summary>
        /// Gets the missing cell.
        /// </summary>
        public bool GetMissingCell(string path, string name)
        {
            var filename = name + ".zip";
            var local = System.IO.Path.Combine(path, filename);

            var Logger = LogProvider.For<SRTMData>();
            Logger.Info($"Downloading {name} ...");
            return SourceHelpers.Download(local, SOURCE + "/" + filename);
        }
    }
}