using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWMClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWMClient.ViewModels
{
    partial class LoginViewModel : ObservableObject
    {
        #region Fileds

        private Models.SWMClient ApiClient;

        #endregion

        #region Propertys

        [ObservableProperty] string _login = "Логин";

        [ObservableProperty] string _password = "Пароль";

        [ObservableProperty] bool _rememberMe = false;

        #endregion

        #region Commands

        [RelayCommand]
        private async Task LogIn()
        {

            var response = ApiClient.LogInAsync(Login, Password);

            if (await response != null)
            {
                Preferences.Set("authorization", "Bearer " + JsonSerializer.Deserialize<Models.JsonModels.Authorization>(await response).access_token);
                Preferences.Set("snowWhiteId", JsonSerializer.Deserialize<Models.JsonModels.Authorization>(await response).snowWhiteId);

                if (RememberMe)
                    Preferences.Set("isRemember", true);

                Login = "Логин";
                Password = "Пароль";
                await Shell.Current.GoToAsync("//MainPage");
            }
        }

        [RelayCommand]
        private async Task Registration()
            => await Shell.Current.GoToAsync("//Registration");


        #endregion

        #region Init

        public LoginViewModel()
        {
            ApiClient = new Models.SWMClient();
        }

        #endregion

    }
}
