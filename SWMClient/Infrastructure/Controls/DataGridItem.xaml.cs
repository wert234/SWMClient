using Microsoft.Maui.Controls;
using SWMClient.Models;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SWMClient.Infrastructure.Controls;

public partial class DataGridItem : ContentView
{

    #region Id

          public static readonly BindableProperty IdProperty
           = BindableProperty.Create(nameof(Id), typeof(int), typeof(DataGridItem));
          public int Id
          {
               get => (int)GetValue(IdProperty);
               set => SetValue(IdProperty, value);
          }

    #endregion

    #region Backgrund 

         public static readonly BindableProperty BackgrundProperty
         = BindableProperty.Create(nameof(Backgrund), typeof(Color), typeof(DataGridItem));
         public Color Backgrund
         {
             get => (Color)GetValue(BackgrundProperty);
             set => SetValue(BackgrundProperty, value);
         }

    #endregion

    #region Items

         public static readonly BindableProperty ItemsProperty
         = BindableProperty.Create(nameof(Items), typeof(IEnumerable<VisualElement>), typeof(DataGridItem));

         public IEnumerable<VisualElement> Items
         {
             get => (IEnumerable<VisualElement>)GetValue(ItemsProperty);
             set => SetValue(ItemsProperty, value);
         } 

    #endregion

    public DataGridItem() => InitializeComponent();

    public virtual void ContentView_Loaded(object sender, EventArgs e)
    {
        DataGridItemInit();
    }

    protected void DataGridItemInit()
    {
        Item.Background = Backgrund;

        foreach (var item in Items)
        {
            var grid = new Grid();
            grid.Children.Add(item);
            grid.WidthRequest = 250;
            grid.ZIndex = 2;
            Item.Add(grid);
        }
    }
}