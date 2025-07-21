using IrzGuard.MVVM.Model;
using IrzGuard.Utility;
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
  public class MainViewModel_Now : INotifyPropertyChanged
  {
    CodeResources resources = new() { Code = 000, DateTimeCreated = DateTime.Now };

    /// <summary>
    /// Событие об изменения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Время создания кода доступа на -1 час.
    /// </summary>
    public DateTime DateTime_Create
    {
      get => this.resources.DateTimeCreated;
      set
      {
        if (!value.Equals(null) && !this.resources.DateTimeCreated.Hour.Equals(value))
        {
          this.resources.DateTimeCreated = value;
          int save_LevelAccess = Hash_table.GetInt("SelectedLVL.config");
          Password = Guard.GeneratePass(save_LevelAccess, value);
          Label_str = $"{value.ToString()}";
        }
      }
    }

    /// <summary>
    /// Код доступа на -1 час.
    /// </summary>
    public int Password
    {
      get => this.resources.Code;
      set
      {
        if (int.IsPositive(value) && !this.resources.Code.Equals(value))
        {
          this.resources.Code = value;
          Label_str = $"{value}";
        }
      }
    }

    private string label_str = "00:00:00 - ---";

    /// <summary>
    /// Строка для вывода на -1 час на экран.
    /// </summary>
    public string Label_str
    {
      get => this.label_str;
      set
      {
        if (!string.IsNullOrEmpty(value) && !label_str.Equals(value))
        {
          this.label_str = $"{this.resources.DateTimeCreated.Hour}:" +
              $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Minute)}:" +
              $"{string.Format("{0:d2}", this.resources.DateTimeCreated.Second)} - " +
              $"{string.Format("{0:d3}", this.resources.Code)}";
          OnPropertyChanged();
        }
      }
    }

    private string str_Version = string.Empty;

    public string Version
    {
      get => this.str_Version;
      set
      {
        if (!string.IsNullOrEmpty(value) && !label_str.Equals(value))
        {
          this.str_Version = $"ООО ИРЗ ТЕК: {value}";
          OnPropertyChanged();
        }
      }
    }

    #region Методы

    /// <summary>
    /// Функция под изменения свойства.
    /// </summary>
    /// <param name="prop"></param>
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    #endregion

    public MainViewModel_Now()
    {
      this.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
  }
}
