
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using SWMClient.Infrastructure.Controls;
using SWMClient.Models;
using SWMClient.Models.Extensions;
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
    public partial class DwarvesViewModel : ObservableObject
    {
        #region Propertys

        [ObservableProperty] ObservableCollection<DataGridItem> data;

        [ObservableProperty] ObservableCollection<Models.JsonModels.DwarvecsTypes> dwarvecsTypes;

        [ObservableProperty] double sortButtonRotationName;

        [ObservableProperty] double sortButtonRotationClass;

        private int searchClass = 11;
        public int SearchClass
        {
            get => searchClass; 
            set
            {
                if (Equals(searchClass, value)) return;
                searchClass = value;
                GridInit(SearchName, SearchClass);
                OnPropertyChanged();
            }
        }

        private string searchName;
        public string SearchName
        {
            get => searchName; 
            set
            {
                if(Equals(searchName, value)) return;
                searchName = value;
                GridInit(SearchName, SearchClass);
                OnPropertyChanged();
            }
        }


        #endregion

        #region Fileds

        private SWMClient.Models.SWMClient client;

        #endregion

        #region Commands

        [RelayCommand]
        private async Task DataAdd()
        {
             await client.CreateDwarfsAsync();
             await GridInit();
        }

        [RelayCommand]
        private async Task DataDelete(object id)
        {
            await client.DeleteDwarfsAsync((int)id);
            await GridInit();
        }

        [RelayCommand]
        async Task SortName()
        {
            SortButtonRotationClass = 0;
            var dwarves = JsonSerializer.Deserialize<IEnumerable<Dwarvecs>>(await client.GetDwarfsAsync());

            if (SortButtonRotationName == 180)
            {
                SortButtonRotationName = 0;
                await GridInit(dwarvesSort: dwarves.OrderByDescending(x => x.name));
            }
            else
            {
                 SortButtonRotationName = 180;
                await GridInit(dwarvesSort: dwarves.OrderBy(x => x.name));
            }
        }

        [RelayCommand]
        async Task SortClass()
        {
            SortButtonRotationName = 0;
            var dwarves = JsonSerializer.Deserialize<IEnumerable<Dwarvecs>>(await client.GetDwarfsAsync());

            if (SortButtonRotationClass == 180)
            {
                SortButtonRotationClass = 0;
                await GridInit(dwarvesSort: dwarves.OrderByDescending(x => x.Class));
            }
            else
            {
                SortButtonRotationClass = 180;
                await GridInit(dwarvesSort: dwarves.OrderBy(x => x.Class));
            }
        }

        #endregion

        #region Init

        public DwarvesViewModel()
        {
            Data = new ObservableCollection<DataGridItem>();

            client = new Models.SWMClient();

            DwarvecsTypesInit();

            GridInit();

        }

        private async Task DwarvecsTypesInit()
        {
            DwarvecsTypes = new ObservableCollection<Models.JsonModels.DwarvecsTypes>();
            var response = await client.GetDwarfsTypesAsync();
            DwarvecsTypes = JsonSerializer.Deserialize<ObservableCollection<Models.JsonModels.DwarvecsTypes>>(response);

            DwarvecsTypes.Add(new Models.JsonModels.DwarvecsTypes() { name = "Все", id = 11 });
        }
        private async Task GridInit(string name = null, int _class = 11, IEnumerable<Dwarvecs> dwarvesSort = null)
        {
            var datas = new ObservableCollection<DataGridItem>();
            IEnumerable<Dwarvecs> dwarves;

            if (dwarvesSort is null)
                dwarves = JsonSerializer.Deserialize<IEnumerable<Dwarvecs>>(await client.GetDwarfsAsync());
            else
                dwarves = dwarvesSort;

            if (name != null)
                dwarves = dwarves.Where(x => x.name.Contains(name));
            if (_class != 11)
                dwarves = dwarves.Where(x => x.Class == _class);

            foreach (Dwarvecs item in dwarves)
            {
                datas.Add(new DataGridDwarvesItem()
                {
                    Id = item.id,
                    Backgrund = Colors.LightGray,
                    Items = new ObservableCollection<VisualElement>()
                    {
                        new DwaresName(){ Text = item.name },
                        new Expander(){ ZIndex = 3, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center,  SelectedItemIndex = item.Class, Items = DwarvecsTypes.Take(10) },
                    }
                });
            }
            Data = datas;   
        } 

        #endregion
    }
}
