using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SWMClient.Infrastructure.Controls;
using SWMClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SWMClient.Models.JsonModels;

namespace SWMClient.ViewModels
{
    public partial class SnowWhitesViewModel : ObservableObject
    {
        #region Fileds

        string searchName;

        Models.SWMClient SWMClient;

        int SelectedDwarvesId = 0;

        #endregion

        #region Propertys
        
        public string SearchName
        {
            get => searchName; 
            set
            {
                searchName = value;
                GridInit(searchName);
                OnPropertyChanged();
            }
        }

        [ObservableProperty] ObservableCollection<DataGridItem> data;

        [ObservableProperty] bool isVisibleDialog = false;

        [ObservableProperty] double opacityContent = 1;

        [ObservableProperty] double sortButtonRotationName;

        #endregion

        #region Commands

        [RelayCommand]
        void Refuse()
        {
            IsVisibleDialog = false;
            OpacityContent = 1;
        }

        [RelayCommand]
        async void Ok()
        {
            await SWMClient.SendRequestsAsync(SelectedDwarvesId);
            Refuse();
        }

        [RelayCommand]
        async void SortName()
        {
            var snowWhites = JsonSerializer.Deserialize<IEnumerable<SnowWhite>>(await SWMClient.GetSnowWhiteAsync());

            if (SortButtonRotationName == 180)
            {
                SortButtonRotationName = 0;
               await GridInit(snowWhiteSort: snowWhites.OrderByDescending(x => x.fullName));
            }
            else
            {
                SortButtonRotationName = 180;
                await GridInit(snowWhiteSort: snowWhites.OrderBy(x => x.fullName));
            }
        }


        #endregion

        #region Init

        public SnowWhitesViewModel()
        {
            SWMClient = new Models.SWMClient();
            GridInit();
        }

        async Task GridInit(string searchName = null, IEnumerable<SnowWhite> snowWhiteSort = null)
        {
            var datas = new ObservableCollection<DataGridItem>();
            var dwarvesTypes = JsonSerializer.Deserialize<IEnumerable<DwarvecsTypes>>(await SWMClient.GetDwarfsTypesAsync());
            IEnumerable<SnowWhite> snowWhites;

            if (snowWhiteSort is null)
                snowWhites = JsonSerializer.Deserialize<IEnumerable<SnowWhite>>(await SWMClient.GetSnowWhiteAsync());
            else
                snowWhites = snowWhiteSort;
                 
            if (searchName != null)
                snowWhites = snowWhites.Where(x => x.fullName.Contains(searchName));

            foreach (var item in snowWhites)
            {
                var dwarves = new TreeView();
                dwarves.Selected += async (object sender) =>
                {
                    var treeView = (TreeView)sender;
                    var snowWhite = JsonSerializer.Deserialize<SnowWhite>(await SWMClient.GetSnowWhiteAsync(item.id));
                    var dwarvesItems = new List<DwaresItem>();

                    foreach (var item in snowWhite.dwarves)
                        dwarvesItems.Add(new DwaresItem(Selected) { ItemName = item.name, ItemClass = dwarvesTypes.ToList()[item.Class].name, ItemId = item.id });

                    treeView.Items = dwarvesItems;
                };

                datas.Add(new DataGridSnowWhiteItem()
                {
                    Id = item.id,
                    Backgrund = Colors.LightGray,
                    Items = new ObservableCollection<VisualElement>()
                    {
                        new Label() { Text = item.fullName },
                        dwarves,
                    }
                });
            }
            Data = datas;
        }

        private void Selected(int dwaresId)
        {
            SelectedDwarvesId = dwaresId;
            IsVisibleDialog = true;
            OpacityContent = 0.5;
        }

        #endregion

    }
}
