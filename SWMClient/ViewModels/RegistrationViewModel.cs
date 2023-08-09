using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.ViewModels
{
    partial class RegistrationViewModel : ObservableObject
    {
        #region Fileds

        private Models.SWMClient _ApiClient;

        #endregion

        #region Propertys

        [ObservableProperty]
        private string _fullname = "ФИО";

        [ObservableProperty]
        private string _login = "Логин";

        [ObservableProperty]
        private string _password = "Пароль";

        #endregion

        #region Commands

        [RelayCommand]
        private async Task LogIn()
            => await Shell.Current.GoToAsync("//login"); 
        

        [RelayCommand]
        private async Task Registration()
        {
            var response = _ApiClient.RegistrationAsync(Login, Password, Fullname);

            if (await response != null)
                await LogIn();
        }

        #endregion

        #region Init

        public RegistrationViewModel() 
            => _ApiClient = new Models.SWMClient();

        #endregion

    }
}
