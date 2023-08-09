using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using SWMClient.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui;
using System.Text.Json;

namespace SWMClient.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        #region Fileds

        private Models.SWMClient SWMClient;

        #endregion

        #region Propertys 

        [ObservableProperty] double menuPositionX;

        [ObservableProperty] VisualElement view =  new Profile();

        [ObservableProperty] ColumnDefinitionCollection columns = new ColumnDefinitionCollection();

        [ObservableProperty] RowDefinitionCollection rows = new RowDefinitionCollection();

        [ObservableProperty] string fullNmae;

        [ObservableProperty] int dworvesCount = 0;

        [ObservableProperty] int snowWhiteCount = 0;

        #endregion

        #region Commands

        [RelayCommand]
        private async Task OpenMenu()
        {
            if (MenuPositionX == -300)
            {
                for (int i = 0; i < 5; i++)
                {
                    MenuPositionX = -300 + (i * 60);
                    await Task.Delay(1);
                }
                MenuPositionX = 0; 
            }
        }

        [RelayCommand]
        private async Task CloseMenu()
        {
            if (MenuPositionX == 0 && (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS))
            {
                for (int i = 0; i > -5; i--)
                {
                    MenuPositionX = (i * 60);
                    await Task.Delay(1);
                }
                MenuPositionX = -300; 
            }
        }

        public ICommand GoToCommand { get; set; }
        private void GoTo(object view)
        {
            switch (view)
            {
                case("Profile"):
                    View = new Profile();
                    break;
                case ("Dwarves"):
                    View = new Dwarves();
                    break;
                case ("SnowWhites"):
                    View = new SnowWhites();
                    break;
                case ("Translations"):
                    View = new Translations();
                    break;

                default:
                    View = new Profile();
                    break;
            }
        }


        public ICommand LogoutCommand { get; set; }
        void Logout()
        {
            Preferences.Clear();
            Shell.Current.GoToAsync("//login");
        }

        #endregion

        #region Init

        public MainPageViewModel()
        {
            SWMClient = new Models.SWMClient();
            GoToCommand = new Command(GoTo);
            LogoutCommand = new Command(Logout);
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                Columns.Add(new ColumnDefinition(new GridLength(300)));
                Columns.Add(new ColumnDefinition());
            }
            else
            {
                Rows.Add(new RowDefinition(new GridLength(50)));
                Rows.Add(new RowDefinition());
            }
            Init();
        }

        private async Task Init()
        {
            var response = await SWMClient.GetSnowWhiteAsync();

            var snowWhites = JsonSerializer.Deserialize<IEnumerable<Models.JsonModels.SnowWhite>>(response);
            SnowWhiteCount = snowWhites.Count();

            response = await SWMClient.GetSnowWhiteAsync(Preferences.Get("snowWhiteId", 1));
            var me = JsonSerializer.Deserialize<Models.JsonModels.SnowWhite>(response);

            FullNmae = me.fullName;
            DworvesCount = me.dwarves.Count();  
        }

        #endregion

    }
}
