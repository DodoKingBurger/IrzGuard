using IrzGuard.MVVM.View;
using IrzGuard.Utility;
using Microsoft.Maui.Controls;
using System.Net.NetworkInformation;

namespace IrzGuard
{
  public partial class MainPage : ContentPage
  {

    private void EntryBox_code_TextChanged(object sender, EventArgs e) 
    {
      EntryBox_code.TextColor = Colors.Black;
    }

    /// <summary>
    /// Событие по нажатию на кнопку.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void CheckingCodePass(object sender, EventArgs e) 
    {
      try
      {
        if (int.TryParse(EntryBox_code.Text, out var result))
        {
          if (Guard.CheckReferencePass(DateTime.Now, result, out int LevelAccess))
          {
            Hash_table.SetInt("SelectedLVL.config", LevelAccess);
            SecurityDevice.SetKey(DeviceSystem.GetCodeDevice());
            Hash_table.SetInt("LevelAccess.config", LevelAccess);
            Button backButton = new Button { Text = "Назад", HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Center };
            backButton.Clicked += async (o, e) => await Navigation.PushAsync(new CodePage(), true);
            ViewContainer.Children.Add(backButton);
            await Navigation.PushAsync(new CodePage(), true);
          }
          else
          {
            EntryBox_code.TextColor = Colors.Red;
          }
        }
        else
        {
          EntryBox_code.TextColor = Colors.Red;
          DisplayAlert("Ошибка", "Не удалось преобразовать введенный код в цифры","Okey");
        }
      }
      catch (Exception ex)
      {
        DisplayAlert("Ошибка", ex.Message, "OK");
      }
    }

    /// <summary>
    /// Проверка верификационного файла.
    /// </summary>
    /// <returns>True, если файл был найден и его содержимое совпадает с ключом.</returns>
    public bool ExistsVerificationFile()
    {
      try
      {
        if (SecurityDevice.EqualsKey(DeviceSystem.GetCodeDevice()))
          return true;
        else
          return false;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    /// <summary>
    /// Загрузка страницы.
    /// </summary>
    async void LoadPage()
    {
      if (ExistsVerificationFile())
      {
        Button backButton = new Button { Text = "Назад", HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Center };
        backButton.Clicked += async (o, e) => await Navigation.PushAsync(new CodePage(),true);
        ViewContainer.Children.Add(backButton);
        await Navigation.PushAsync(new CodePage()); 
      } 
    }


    public MainPage()
    {

      InitializeComponent();
      LoadPage();
    }
  }

}
