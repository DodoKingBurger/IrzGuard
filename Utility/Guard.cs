using System.Reflection.Metadata.Ecma335;

namespace IrzGuard.Utility
{
  /// <summary>
  /// Защитник, будет генерировать пароль, а также проверять переданный код.
  /// </summary>
  public static class Guard
  {
    #region Поля и свойсвта

    private static int pass = 000;

    /// <summary>
    /// Пароль.
    /// </summary>
    public static int Pass
    {
      get => pass;
      set
      {
        if (int.IsPositive(value) && value <= 999)
        {
          pass = value;
        }
        else
          pass = 000;
      }
    }

    #endregion

    #region  Методы 

    /// <summary>
    /// Возвращает пароль в трехзначном формате ("000").
    /// </summary>
    /// <returns>Пароль.</returns>
    public static string GetPass()
    {
      return string.Format("{0:d3}", Pass);
    }

    /// <summary>
    /// Генератор пароля.
    /// </summary>
    /// <param name="LevelAccess">Уровень доступа (0..2)</param>
    /// <param name="dateTime">Дата и время для генерации</param>
    /// <returns> Трехзначный пароль если есть такой уровень доступа, иначе 0.</returns>
    public static int GeneratePass(int LevelAccess, DateTime dateTime)
    {
      if (LevelAccess < 0 || LevelAccess >= 3 || dateTime == null)
        return 000;

      int Base = (dateTime.Date.Year % 100 * dateTime.Date.Month * dateTime.Day * dateTime.Hour) % 1000;
      int Key = (dateTime.Date.Year % 100 + dateTime.Date.Month + dateTime.Day + dateTime.Hour) % 1000;

      //Сдвиг по уровню доступа
      int ditgit1 = (Base / 100 + LevelAccess) % 10;
      int ditgit2 = (Base / 10 % 10 + LevelAccess) % 10;
      int ditgit3 = (Base % 10 + LevelAccess) % 10;

      //XOR с ключом.
      Pass = (ditgit1 * 100 + ditgit2 * 10 + ditgit3 ^ Key) % 1000;

      return Pass;
    }

    /// <summary>
    /// Проверка пароля.
    /// </summary>
    /// <param name="dateTime">Время.</param>
    /// <param name="CheckingPass">Проверяемый пароль.</param>
    /// <param name="LevelAccess">Уровень доступа полученный.</param>
    /// <returns>True если пароль прошел проверку, иначе False.</returns>
    public static bool CheckPass(DateTime dateTime, int CheckingPass, out int LevelAccess)
    {
      for (int i = 0; i < 3; i++)
      {
        if (Equals(GeneratePass(i, dateTime), CheckingPass))
        {
          LevelAccess = i;
          return true;
        }
      }
      LevelAccess = -1;
      return false;
    }

    /// <summary>
    /// Проверка пароля.
    /// </summary>
    /// <param name="dateTime">Время.</param>
    /// <param name="CheckingPass">Проверяемый пароль.</param>
    /// <param name="LevelAccess">Уровень доступа полученный.</param>
    /// <returns>True если пароль прошел проверку, иначе False.</returns>
    public static bool CheckReferencePass(DateTime dateTime, int CheckingPass, out int LevelAccess)
    {
      for (int i = 0; i < 3; i++)
      {
        int codenow = GenerateReferenceCode(i, dateTime);
        if (Equals(codenow, CheckingPass))
        {
          LevelAccess = i;
          return true;
        }
      }
      LevelAccess = -1;
      return false;
    }


    /// <summary>
    /// Генератор код для активации в IrzGuard.
    /// </summary>
    /// <param name="LevelAccess">Уровень доступа (0..2)</param>
    /// <param name="dateTime">Дата и время для генерации</param>
    /// <returns> Трехзначный код если есть такой уровень доступа, иначе 0.</returns>
    public static int GenerateReferenceCode(int LevelAccess, DateTime dateTime)
    {
      if (LevelAccess < 0 || LevelAccess >= 3 || dateTime == null)
        return 000;

      int Base = ((dateTime.Date.Year % 100) * dateTime.Date.Month * dateTime.Day * dateTime.Hour * (dateTime.Minute / 10)) % 1000;
      int Key = (dateTime.Date.Year % 100 + dateTime.Date.Month + dateTime.Day + dateTime.Hour + dateTime.Minute / 10) % 1000;

      //Сдвиг по уровню доступа
      int ditgit1 = (Base / 100 + LevelAccess) % 10;
      int ditgit2 = (Base / 10 % 10 + LevelAccess) % 10;
      int ditgit3 = (Base % 10 + LevelAccess) % 10;

      return (ditgit1 * 100 + ditgit2 * 10 + ditgit3 ^ Key) % 1000;
    }


    #endregion

    #region Конструкторы

    //public Guard() { }

    #endregion
  }
}
