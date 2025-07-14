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
        if (!File.Exists(Path.Combine(mainDir, key)))
          return "---";
        using var stream = File.OpenRead(Path.Combine(mainDir, key));
        using var reader = new StreamReader(stream);
        var contents = reader.ReadToEnd();

        if (!string.IsNullOrEmpty(contents))
        {
          reader.Close();
          return contents.Trim();
        }
        else
        {
          reader.Close();
          return "---";
        }
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
        FileStream stream;
        if (!File.Exists(Path.Combine(mainDir, key)))
          stream = File.Create(Path.Combine(mainDir, key));
        else
          stream = File.OpenWrite(Path.Combine(mainDir, key));
        if (!string.IsNullOrEmpty(value) && stream.CanWrite)
        {
          using var writer = new StreamWriter(stream);
          writer.WriteLine($"{value}");
          writer.Close();
        }
        else
          throw new ArgumentException($"{value} - int is null.");
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
        if (!File.Exists(Path.Combine(mainDir, key)))
          return -1;

        using var stream = File.OpenRead(Path.Combine(mainDir, key));
        using var reader = new StreamReader(stream);
        if (int.TryParse(reader.ReadToEnd(), out var contents))
        {
          reader.Close();
          return contents;
        }
        else
        {
          reader.Close();
          return -1;
        }
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
        FileStream stream;
        if (!File.Exists(Path.Combine(mainDir, key)))
          stream = File.Create(Path.Combine(mainDir, key));
        else
          stream = File.OpenWrite(Path.Combine(mainDir, key));
        if (!value.Equals(null) && stream.CanWrite)
        {
          using var writer = new StreamWriter(stream);
          writer.WriteLine($"{value}");
          writer.Close();
        }
        else
          throw new ArgumentException($"{value} - int is null.");
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
        File.Create(Path.Combine(mainDir, key));
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
