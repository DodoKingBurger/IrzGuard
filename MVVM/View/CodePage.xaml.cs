using IrzGuard.MVVM.ViewModel;
using IrzGuard.Utility;

namespace IrzGuard.MVVM.View;

public partial class CodePage : ContentPage
{
  #region Поля и свойства

  /// <summary>
  /// Таймер.
  /// </summary>
  IDispatcherTimer timer_minute = Application.Current.Dispatcher.CreateTimer();

  /// <summary>
  /// ViewModel ну тип косячный, но вариант Модель он не видит значит работает ))))
  /// </summary>
  public MainViewModel labelViewModel = new MainViewModel();

  private int LevelAccess_now = -1;

  #endregion

  #region Методы

  /// <summary>
  /// Выбор доступного уровня допуска.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ComboBox_AccessLevel_SelectedIndexChanged(object sender, EventArgs e) 
	{
    if (sender is Picker)
      Hash_table.SetInt("SelectedLVL.config", ((Picker)sender).SelectedIndex);
  }

	/// <summary>
	/// Возвращение на страницу с вводом кода.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private async void ReloadLevelAccess(object sender, EventArgs e) 
	{
    await Navigation.PopAsync(true);
  }

  /// <summary>
  /// Выдает список взависимости от уровня доступа.
  /// </summary>
  /// <returns>Список от уровня доступа.</returns>
  public List<string> GetLvlAccess()
  {
    var list = new List<string>();
    int count = Hash_table.GetInt("LevelAccess.config");
    foreach (EnumLvlAccess item in Enum.GetValues(typeof(EnumLvlAccess)))
    {
      if ((int)item <= count)
        list.Add(item.ToString());
      else
        break;
    }
    return list;
  }

  /// <summary>
  /// Загрузка старницы.
  /// </summary>
  private void LoadPage() 
  {
    BindingContext = labelViewModel;
    ComboBox_AccessLevel.ItemsSource = GetLvlAccess();
    ComboBox_AccessLevel.SelectedIndex = Hash_table.GetInt("SelectedLVL.config");
    timer_minute.Interval = TimeSpan.FromSeconds(1);
    //timer_hour.Interval = TimeSpan.FromHours(1);

    timer_minute.Tick += (s, e) => ViewCodePass();
    timer_minute.Start();
  }

  /// <summary>
  /// Обновление кодов(тик таймера).
  /// </summary>
  private void ViewCodePass() 
  {
    try
    {
      if(this.LevelAccess_now != Hash_table.GetInt("LevelAccess.config")) 
      {
        this.LevelAccess_now = Hash_table.GetInt("LevelAccess.config");
        ComboBox_AccessLevel.ItemsSource = GetLvlAccess();
        ComboBox_AccessLevel.SelectedIndex = Hash_table.GetInt("LevelAccess.config");
        Hash_table.SetInt("SelectedLVL.config", this.LevelAccess_now);
      }

      int save_LevelAccess = Hash_table.GetInt("SelectedLVL.config");
      DateTime dateTime = DateTime.Now;

      labelViewModel.Now_date.DateTime_Create = dateTime;
      labelViewModel.Past_data.DateTime_Create = dateTime.AddHours(-1);
      labelViewModel.Future_data.DateTime_Create = dateTime.AddHours(1);
    }
    catch (Exception ex)
    {
      DisplayAlert("Ошибка", $"{ex.Message}\n Продолжить ?", "Yes", "No");
    }
  }

  #endregion

  #region Констуркторы

  public CodePage()
	{
		InitializeComponent();
    LoadPage();
  }

  #endregion
}