using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.MVVM.Model
{
  /// <summary>
  /// Информация для формирования кода доступа.
  /// </summary>
  public class CodeResources
  {
    /// <summary>
    /// Время сформирования кода.
    /// </summary>
    public DateTime DateTimeCreated { get; set; } = DateTime.Now;

    /// <summary>
    /// Сформированный трехзначный код для доступа к КСУ или приглашения коллеги.
    /// </summary>
    public int Code { get; set; }
  }
}
