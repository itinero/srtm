using SRTM.Logging;
using System;
using System.IO;
using System.Net.Http;

namespace SRTM.Sources
{
    public class SourceHelpers
    {
        /// <summary>
        /// Donwloads a remote file and stores the data in the local one.
        /// </summary>
        public static bool Download(string local, string remote, bool logErrors = false)
        {
            var Logger = LogProvider.For<SourceHelpers>();

            try
            {
                if (File.Exists(local))
                {
                    File.Delete(local);
                }

                var client = new HttpClient();
                using (var stream = client.GetStreamAsync(remote).Result)
                using (var outputStream = File.OpenWrite(local))
                {
                    stream.CopyTo(outputStream);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (logErrors)
                {
                    Logger.ErrorException("Download failed.", ex);
                }
            }
            return false;
        }
    }
}
