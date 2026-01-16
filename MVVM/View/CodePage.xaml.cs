using IrzGuard.MVVM.ViewModel;
using IrzGuard.Utility;

namespace IrzGuard.MVVM.View;

public partial class CodePage : ContentPage
{
  #region Поля и свойства

  /// <summary>
  /// Таймер.
  /// </summary>
  private IDispatcherTimer timer_minute = Application.Current.Dispatcher.CreateTimer();

  /// <summary>
  /// ViewModel ну тип косячный, но вариант Модель он не видит значит работает ))))
  /// </summary>
  public MainViewModel labelViewModel = new();

  /// <summary>
  /// Уровень доступа сейчас.
  /// </summary>
  private int LevelAccess_now = -1;

	private string str_Version = string.Empty;

	public string Version
	{
		get => this.str_Version;
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.str_Version = $"ООО ИРЗ ТЕК: {value}";
				OnPropertyChanged();
			}
		}
	}

	#endregion

	#region Методы

	/// <summary>
	/// Выбор доступного уровня допуска.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ComboBox_AccessLevel_SelectedIndexChanged(object sender, EventArgs e) 
	{
    if (sender is Picker picker)
      Hash_table.SetInt("SelectedLVL.config", picker.SelectedIndex);
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
  /// Загрузка старницы.
  /// </summary>
  private void LoadPage() 
  {
    BindingContext = labelViewModel;
    ComboBox_AccessLevel.SelectedIndex = Hash_table.GetInt("SelectedLVL.config");
    this.timer_minute.Interval = TimeSpan.FromSeconds(1);
    this.timer_minute.Tick += (s, e) => ViewCodePass();
    this.timer_minute.Start();
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
        ComboBox_AccessLevel.ItemsSource = labelViewModel.ListLevelAccess;
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
      if (!DisplayAlert("Ошибка", $"{ex.Message}\n Продолжить ?", "Yes", "No").Result)
        this.timer_minute.Stop();
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