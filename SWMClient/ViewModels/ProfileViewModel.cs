using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWMClient.ViewModels
{
    public class ProfileViewModel : ObservableObject
    {

        #region Fileds

        private Models.SWMClient SWMClient;

        #endregion

        #region Propertys

        private string fullName;

        public string FullName
        {
            get => fullName; 
            set
            {
                if(Equals(fullName, value)) return;
                fullName = value;
                SWMClient.EditNameSnowWhiteAsync(FullName);
                OnPropertyChanged();
            }
        }

        #endregion

        #region Init

        public ProfileViewModel()
        {
            SWMClient = new Models.SWMClient();
            Init();
        }

        private async Task Init()
        {
            var response = await SWMClient.GetSnowWhiteAsync(Preferences.Get("snowWhiteId", 1));
            var SnowWhite = JsonSerializer.Deserialize<Models.JsonModels.SnowWhite>(response);
            FullName = SnowWhite.fullName;
        }

        #endregion

    }
}
