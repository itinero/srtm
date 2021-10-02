using SRTM.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace SRTM.Sources
{
  public class SourceHelpers
  {
    /// <summary>
    /// Downloads a remote file and stores the data in the local one.
    /// </summary>
    public static bool Download(string local, string remote, bool logErrors = false)
    {
      var client = new HttpClient();
      return PerformDownload(client, local, remote, logErrors);
    }

    /// <summary>
    /// Downloads a remote file and stores the data in the local one. The given credentials are used for authorization.
    /// </summary>
    public static bool DownloadWithCredentials(NetworkCredential credentials, string local, string remote,
        bool logErrors = false)
    {
      // Ideally the cookie container will be persisted to/from file
      CookieContainer myContainer = new CookieContainer();

      // Create a credential cache for authenticating when redirected to Earthdata Login
      CredentialCache cache = new CredentialCache();
      cache.Add(new Uri(credentials.Domain), "Basic", new NetworkCredential(credentials.UserName, credentials.Password));

      // Modify the simple client handler to using cached Credentials
      HttpClientHandler handler = new HttpClientHandler();
      handler.Credentials = cache;
      handler.CookieContainer = myContainer;
      handler.PreAuthenticate = false;
      handler.AllowAutoRedirect = true;

      var client = new HttpClient(handler);

      return PerformDownload(client, local, remote, logErrors);
    }

    private static bool PerformDownload(HttpClient client, string local, string remote, bool logErrors = false)
    {
      var Logger = LogProvider.For<SourceHelpers>();

      try
      {
        if (File.Exists(local))
        {
          File.Delete(local);
        }

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
