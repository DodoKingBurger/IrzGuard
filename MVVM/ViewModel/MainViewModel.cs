using IrzGuard.MVVM.Model;
using IrzGuard.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuard.MVVM.ViewModel
{
  /// <summary>
  /// ViewModel Главного экрана.
  /// </summary>
  public class MainViewModel : INotifyPropertyChanged
  {
    #region Поля и свойства

    /// <summary>
    /// Уровни доступа.
    /// </summary>
    public List<string> ListLevelAccess = ["Электромонтер", "Мастер", "Администратор"];

    /// <summary>
    /// Данные для генерации на экран кодов доступа.
    /// </summary>
    public List<PasswordResources> resources = new()
        {
            new PasswordResources(){ Password = 000, DateTimeCreated = DateTime.Now},
            new PasswordResources(){ Password = 000, DateTimeCreated = DateTime.Now},
            new PasswordResources(){ Password = 000, DateTimeCreated = DateTime.Now}
        };

    /// <summary>
    /// Событие об изменения свойства.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Время создания кода доступа на -1 час.
    /// </summary>
    public DateTime DateTime_Create_Past
    {
      get => this.resources[0].DateTimeCreated;
      set
      {
        if (!value.Equals(null) && !this.resources[0].DateTimeCreated.Hour.Equals(value))
        {
          this.resources[0].DateTimeCreated = value;
          Label_str_Past = $"{value.ToString()}";
        }
      }
    }

    /// <summary>
    /// Код доступа на -1 час.
    /// </summary>
    public int Password_Past
    {
      get => this.resources[0].Password;
      set
      {
        if (int.IsPositive(value) && !this.resources[0].Password.Equals(value))
        {
          this.resources[0].Password = value;
          Label_str_Past = $"{value}";
        }
      }
    }

    private string label_str_Past = "00:00:00 - ---";

    /// <summary>
    /// Строка для вывода на -1 час на экран.
    /// </summary>
    public string Label_str_Past
    {
      get => this.label_str_Past;
      set
      {
        if (!string.IsNullOrEmpty(value) && !label_str_Past.Equals(value))
        {
          this.label_str_Past = $"{this.resources[0].DateTimeCreated.Hour}:" +
              "00:" +
              "00 - " +
              $"{string.Format("{0:d3}", this.resources[0].Password)}";
          OnPropertyChanged();
        }
      }
    }


    /// <summary>
    /// Время создания текущего кода доступа.
    /// </summary>
    public DateTime DateTime_Create_Now
    {
      get => this.resources[1].DateTimeCreated;
      set
      {
        if (!value.Equals(null) && !this.resources[1].DateTimeCreated.Equals(value))
        {
          this.resources[1].DateTimeCreated = value;
          Label_str_Now = $"{value.ToString()}";
        }
      }
    }

    /// <summary>
    /// Текущий код доступа.
    /// </summary>
    public int Password_Now
    {
      get => this.resources[1].Password;
      set
      {
        if (int.IsPositive(value) && !this.resources[1].Password.Equals(value))
        {
          this.resources[1].Password = value;
          Label_str_Now = $"{value}";
        }
      }
    }

    private string label_str_Now = "00:00:00 - ---";

    /// <summary>
    /// Строка для вывода на экран с текущим временем и кодом доступа.
    /// </summary>
    public string Label_str_Now
    {
      get => this.label_str_Now;
      set
      {
        if (!string.IsNullOrEmpty(value) && !label_str_Now.Equals(value))
        {
          this.label_str_Now = $"{this.resources[1].DateTimeCreated.Hour}:" +
              $"{string.Format("{0:d2}", this.resources[1].DateTimeCreated.Minute)}:" +
              $"{string.Format("{0:d2}", this.resources[1].DateTimeCreated.Second)} - " +
              $"{string.Format("{0:d3}", this.resources[1].Password)}";
          OnPropertyChanged();
        }
      }
    }

    /// <summary>
    /// Время создания кода доступа на +1 час.
    /// </summary>
    public DateTime DateTime_Create_Future
    {
      get => this.resources[2].DateTimeCreated;
      set
      {
        if (!value.Equals(null) && !this.resources[2].DateTimeCreated.Hour.Equals(value))
        {
          this.resources[2].DateTimeCreated = value;
          Label_str_Future = $"{value.ToString()}";
        }
      }
    }

    /// <summary>
    /// Код доступа на +1 час .
    /// </summary>
    public int Password_Future
    {
      get => this.resources[2].Password;
      set
      {
        if (int.IsPositive(value) && !this.resources[2].Password.Equals(value))
        {
          this.resources[2].Password = value;
          Label_str_Future = $"{value}";
        }
      }
    }

    private string label_str_Future = "00:00:00 - ---";

    /// <summary>
    /// Строка с кодом доступа на +1 час для вывода на экран.
    /// </summary>
    public string Label_str_Future
    {
      get => this.label_str_Future;
      set
      {
        if (!string.IsNullOrEmpty(value) && !label_str_Future.Equals(value))
        {
          this.label_str_Future = $"{this.resources[2].DateTimeCreated.Hour}:" +
              "00:" +
              "00 - " +
              $"{string.Format("{0:d3}", this.resources[2].Password)}";
          OnPropertyChanged();
        }
      }
    }

    #endregion

    #region NOT USED

    //public Label_str label = new Label_str 
    //{
    //    Now = "00:00:00 - ---",
    //    Past = "00:00 - ---",
    //    Future = "00:00 - ---"
    //};

    //public string Name_Past 
    //{
    //    get => label.Past; 
    //    set
    //    {
    //        if (!string.IsNullOrEmpty(value) && !label.Past.Equals(value))
    //        {
    //            label.Past = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}
    //public string Name_Now
    //{
    //    get => label.Now;
    //    set
    //    {
    //        if (!string.IsNullOrEmpty(value) && !label.Past.Equals(value))
    //        {
    //            label.Now = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}
    //public string Name_Future
    //{
    //    get => label.Future;
    //    set
    //    {
    //        if (!string.IsNullOrEmpty(value) && !label.Past.Equals(value))
    //        {
    //            label.Future = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //}

    #endregion

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
  }
}
