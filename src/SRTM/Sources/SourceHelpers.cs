using SRTM.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace SRTM.Sources
{
    public static class SourceHelpers
    {
        public static bool Download(string local, string remote)
        {
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
            }
            catch (Exception ex)
            {
                LogProvider.CurrentLogProvider.GetLogger()
            }
            return false;
        }
    }
}
