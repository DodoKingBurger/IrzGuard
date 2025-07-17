using IrzGuard.MVVM.Model;
using IrzGuard.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace IrzGuard.MVVM.ViewModel
{
  /// <summary>
  /// ViewModel Главного экрана.
  /// </summary>
  public class MainViewModel
  {
    #region Поля и свойства

    /// <summary>
    /// Уровни доступа.
    /// </summary>
    public List<string> ListLevelAccess;//= ["Электромонтер", "Мастер", "Администратор"];

    /// <summary>
    /// ViewModel с данными на данный момент.
    /// </summary>
    public MainViewModel_Now Now_date { get; set; }

    /// <summary>
    /// ViewModel с данными на момент +1 час от текущего времени.
    /// </summary>
    public MainViewModel_Modified Future_data { get; set; }

    /// <summary>
    /// ViewModel с данными на момент -1 час от текущего времени.
    /// </summary>
    public MainViewModel_Modified Past_data { get; set; }

    #endregion

    #region Функции

    /// <summary>
    /// Выдает список взависимости от уровня доступа.
    /// </summary>
    /// <returns>Список от уровня доступа.</returns>
    public void GetLvlAccess()
    {
      //var list = new List<string>();
      this.ListLevelAccess = new List<string>();
      int count = Hash_table.GetInt("LevelAccess.config");
      foreach (EnumLvlAccess item in Enum.GetValues(typeof(EnumLvlAccess)))
      {
        if ((int)item <= count)
          this.ListLevelAccess.Add(item.ToString());
        else
          break;
      }
    }

    #endregion

    #region Конструкторы

    public MainViewModel()
    {
      this.Now_date = new MainViewModel_Now();
      this.Future_data = new MainViewModel_Modified();
      this.Past_data = new MainViewModel_Modified();
      GetLvlAccess();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="list">Список уровней доступа.</param>
    public MainViewModel(List<string> list)
    {
      this.ListLevelAccess = list;
      this.Now_date = new MainViewModel_Now();
      this.Future_data = new MainViewModel_Modified();
      this.Past_data = new MainViewModel_Modified();
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
  }
}
