using System;
using System.IO;

namespace SRTM.Tests.Functional
{
  /// Author    : Kurniawan
  /// Desc      : Class untuk membaca override setting project per file
  /// Modified  : 2018-09-03
  public class SettingReader
  {

    /// <summary>
    /// Read a setting from a predefined file
    /// </summary>
    /// <param name="file_name"></param>
    /// <param name="default_value"></param>
    /// <returns></returns>
    public static string ReadSettingFile(string file_name, string default_value = "")
    {
      string setting = default_value;
      string setting_file_location = AppDomain.CurrentDomain.BaseDirectory + file_name;
      try
      {
        setting = File.ReadAllText(setting_file_location);
      }
      catch (Exception)
      {
        //Console.WriteLine(e.ToString());
        Console.Write("console:message >> ");
        Console.WriteLine(setting_file_location + " not found");
      }
      return setting;
    }
  }
}