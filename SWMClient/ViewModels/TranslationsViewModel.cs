using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWMClient.Infrastructure.Controls;
using SWMClient.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWMClient.ViewModels
{
    public partial class TranslationsViewModel : ObservableObject
    {
        #region Fileds

        private Models.SWMClient _swmClient;

        #endregion

        #region Propertys

        [ObservableProperty] ObservableCollection<DataGridItem> data;

        #endregion

        #region Commands

        [RelayCommand]
        async Task Accept(int id)
        {
            await _swmClient.SendAnswerAsync(id, true);
            await GridInit();
        }

        [RelayCommand]
        async Task Refuse(int id)
        {
             await _swmClient.SendAnswerAsync(id, false);
             await GridInit();
        }

        #endregion


        #region Init

        public TranslationsViewModel()
        {
            _swmClient = new Models.SWMClient();
            GridInit();
        }

        private async Task GridInit()
        {
            var datas = new ObservableCollection<DataGridItem>();

            var response = await _swmClient.GetRequestsAsync();
            var reqvests = JsonSerializer.Deserialize<IEnumerable<DwarvesReqvest>>(response);

            foreach (DwarvesReqvest item in reqvests)
            {
                datas.Add(new DataGridItem()
                {
                    Id = item.id,
                    Backgrund = Colors.LightGray,
                    Items = new List<VisualElement>()
                    {
                        new Label(){ Text = item.snowWhiteFullName },
                        new Label(){ Text = item.dwarfName },
                    }
                });
            }
            Data = datas;
        }

        #endregion
    }
}
