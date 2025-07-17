using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.Utility
{
  /// <summary>
  /// База для хранения данных в файле.
  /// </summary>
  public static class Hash_table
  {
    /// <summary>
    /// Дирректория в которой расположен .exe.
    /// </summary>
    private static string mainDir = FileSystem.Current.AppDataDirectory;

    /// <summary>
    /// Возвращает строку по названию файла.
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <returns>Строка внутри файла.</returns>
    /// <exception cref="ArgumentException">Названия файла null.</exception>
    public static string GetString(string key)
    {
      if (!string.IsNullOrEmpty(key))
      {
        string contents = string.Empty;
        using (FileStream fs = new FileStream(Path.Combine(mainDir, key), FileMode.OpenOrCreate))
        {
          StreamReader reader = new StreamReader(fs);
          contents = reader.ReadToEnd().Trim();
          if (string.IsNullOrEmpty(contents))
            contents = "---";
        }
        return contents;
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <param name="value">Значения передаваймая в файл.</param>
    /// <exception cref="ArgumentException">value или key null</exception>
    /// <exception cref="FileLoadException">Файл не получилось перезаписать.</exception>
    public static void SetString(string key, string value)
    {
      if (!string.IsNullOrEmpty(key))
      {
        using (FileStream fs = new FileStream(Path.Combine(mainDir, key), FileMode.Create))
        {
          StreamWriter writer = new StreamWriter(fs);
          writer.WriteLine(value);
        }
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <returns>Число внутри файла.</returns>
    /// <exception cref="ArgumentException">Названия файла null.</exception>
    public static int GetInt(string key)
    {
      if (!string.IsNullOrEmpty(key))
      {
        int value = -1;
        using (FileStream fs = new FileStream(Path.Combine(mainDir, key), FileMode.OpenOrCreate))
        {
          StreamReader reader = new StreamReader(fs);
          int.TryParse(reader.ReadToEnd().Trim(), out value);
        }
        return value;
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <param name="value">Значения передаваймая в файл.</param>
    /// <exception cref="ArgumentException">value или key null</exception>
    public static void SetInt(string key, int value)
    {
      if (!string.IsNullOrEmpty(key))
      {
        using (FileStream fs = new FileStream(Path.Combine(mainDir, key), FileMode.Create))
        {
          StreamWriter writer = new StreamWriter(fs);
          writer.WriteLine(value);
        }
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// Созадет файл.
    /// </summary>
    /// <param name="key"></param>
    public static void CreateFile(string key)
    {
      if (!File.Exists(Path.Combine(mainDir, key)))
      {
        FileStream stream = File.Create(Path.Combine(mainDir, key));
        stream.Close();
      }
    }

    /// <summary>
    /// проверяет существует ли файл.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool Exists(string key)
    {
      return File.Exists(Path.Combine(mainDir, key));
    }
  }
}
