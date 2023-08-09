using SWMClient.Models.JsonModels;
using SWMClient.Views;
using System.Diagnostics;
using System.Text.Json;

namespace SWMClient.Infrastructure.Controls;

public partial class DwaresItem : ContentView
{

    #region Fileds

    public event Action<int> Selected;

    #endregion

    #region Propertys

    public static readonly BindableProperty ItemIdProperty =
    BindableProperty.Create("ItemId", typeof(int), typeof(DwaresItem));
    public int ItemId
    {
        get => (int)GetValue(ItemIdProperty);
        set => SetValue(ItemIdProperty, value);
    }


    public static readonly BindableProperty ItemNameProperty =
    BindableProperty.Create("ItemName", typeof(string), typeof(DwaresItem));
    public string ItemName
    {
        get => GetValue(ItemNameProperty) as string;
        set => SetValue(ItemNameProperty, value);
    }

    public static readonly BindableProperty ItemClassProperty =
    BindableProperty.Create("ItemClass", typeof(string), typeof(DwaresItem));
    public string ItemClass
    {
        get => GetValue(ItemClassProperty) as string;
        set => SetValue(ItemClassProperty, value);
    }

    #endregion

    public DwaresItem(object v) => InitializeComponent();
    public DwaresItem(Action<int> Selected)
    {
        InitializeComponent();
        this.Selected = Selected;
        
    }

    private void ContentView_Loaded(object sender, EventArgs e)
    {
        var control = sender as DwaresItem;

        control.Name.Text = ItemName;
        control.Class.Text = ItemClass;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) => Selected?.Invoke(ItemId);
}