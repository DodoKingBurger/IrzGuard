using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.MVVM.ViewModel
{
	internal class StartPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public string Version
		{
			get => $"ИРЗ ТЕК: {Assembly.GetExecutingAssembly().GetName().Version}";
		}

		#region Методы

		/// <summary>
		/// Функция под изменения свойства.
		/// </summary>
		/// <param name="prop"></param>
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		#endregion
	}
}
