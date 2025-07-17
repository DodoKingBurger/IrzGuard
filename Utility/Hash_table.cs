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
    private static readonly string mainDir = FileSystem.Current.AppDataDirectory;

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
        if (!File.Exists(Path.Combine(mainDir, key)))
          return "---";
        using (FileStream fs = File.OpenRead(Path.Combine(mainDir, key)))
        {
          StreamReader reader = new(fs);
          contents = reader.ReadToEnd().Trim();

        }
        if (string.IsNullOrEmpty(contents))
          contents = "---";
        return contents;
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// Перезаписывает файл с названием key, и записывает туда данные из параметра value.
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <param name="value">Значения передаваймая в файл.</param>
    /// <exception cref="ArgumentException">value или key null</exception>
    /// <exception cref="FileLoadException">Файл не получилось перезаписать.</exception>
    public static async void SetString(string key, string value)
    {
      if (!string.IsNullOrEmpty(key))
      {
        using (StreamWriter writer = new(Path.Combine(mainDir, key), false))
        {
          await writer.WriteLineAsync(value);
        }
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// Возвращает из файла key число, если конвертировать не получается вернет -1.
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <returns>Число внутри файла.</returns>
    /// <exception cref="ArgumentException">Названия файла null.</exception>
    public static int GetInt(string key)
    {
      if (!string.IsNullOrEmpty(key))
      {
        int value = -1;
        if (!File.Exists(Path.Combine(mainDir, key)))
          return value;
        using (StreamReader reader = new(Path.Combine(mainDir, key)))
        {
          string content = reader.ReadToEnd();
          int.TryParse(content.Trim(), out value);
        }
        return value;
      }
      else
        throw new ArgumentException($"{key} - string is null or empty.");
    }

    /// <summary>
    /// Перезаписывает файл с названием key, и записывает туда данные из параметра value.
    /// </summary>
    /// <param name="key">Имя файла.</param>
    /// <param name="value">Значения передаваймая в файл.</param>
    /// <exception cref="ArgumentException">value или key null</exception>
    public static async void SetInt(string key, int value)
    {
      if (!string.IsNullOrEmpty(key))
      {
        using (StreamWriter writer = new(Path.Combine(mainDir, key), false))
        {
          await writer.WriteLineAsync($"{value}");
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
