using IrzGuard.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.Utility
{
  public static class SecurityDevice
  {
    public static void SetKey(string str) 
    {
      Hash_table.SetString("CodePass.config", str);
    }

    /// <summary>
    /// Метод шифрудет данные с указаным публичным  ключем
    /// </summary>
    /// <param name="text">Текс для расшидрования</param>
    /// <returns>Закодированная строка.</returns>
    public static string Encrypt(string text)
    {
      text = text.ToLower();
      int total = 0;

      foreach (char c in text)
      {
        int num;
        if (char.IsDigit(c))
        {
          num = c - '0';
        }
        else if (char.IsLetter(c))
        {
          num = c - 'a' + 10;
        }
        else
        {
          continue; // Пропускаем недопустимые символы
        }
        total = (total * 33 + num) % 10000; // Хеширование
      }
      string code = string.Format("{0:d4}", total);
      return code;
    }


    public static bool EqualsKey(string str) 
    {
      
      string codePass = Hash_table.GetString("CodePass.config");

      if(string.IsNullOrEmpty(codePass) || (codePass == "---") || string.IsNullOrEmpty(str))
        return false;

      if (Encrypt(codePass).Equals(Encrypt(str))) 
        return true;
      else
        return false;
    }
  }
}
