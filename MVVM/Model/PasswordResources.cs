using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.MVVM.Model
{
    /// <summary>
    /// Информация о коде для приглашения пользователя.
    /// </summary>
    public class PasswordResources
    {
        /// <summary>
        /// Время сформирования кода.
        /// </summary>
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;

        /// <summary>
        /// Сформированный код для приглашения.
        /// </summary>
        public int Password { get; set; }
    }
}
