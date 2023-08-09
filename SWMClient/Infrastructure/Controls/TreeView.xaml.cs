using Microsoft.Maui.Controls;
using SWMClient.Models.JsonModels;
using System.Collections;
using System.Text.Json;

namespace SWMClient.Infrastructure.Controls;

public partial class TreeView : ContentView
{

    #region Fileds

    public event Action<object> Selected;

    #endregion

    #region Propertys

    #region IsVisible

    public static readonly BindableProperty IsVisibleProperty
	= BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(TreeView), false, BindingMode.TwoWay, propertyChanged: IsVisiblePropertyChanged);

    private static void IsVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
       var control = bindable as TreeView;
       control.Scroll.IsVisible = (bool)newValue;
        control.Content.Clear();
    }

    public bool IsVisible
	{
		get => (bool)GetValue(IsVisibleProperty);
		set => SetValue(IsVisibleProperty, value);
	}

    #endregion

    #region Items

    public static readonly BindableProperty ItemsProperty
    = BindableProperty.Create(nameof(Items), typeof(IEnumerable<VisualElement>), typeof(TreeView), null, BindingMode.TwoWay, propertyChanged: ItemsPropertyChanged);

    private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as TreeView;
        DwarvesInit(newValue, control.Content);
    }

    public IEnumerable<VisualElement> Items
    {
        get => (IEnumerable<VisualElement>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    #endregion

    #endregion

    #region Init
    public TreeView() => InitializeComponent();

    private void ContentView_Loaded(object sender, EventArgs e)
    {
        DwarvesInit(Items, Content);
        Scroll.IsVisible = IsVisible;
    }

    private static void DwarvesInit(object newValue, VerticalStackLayout control)
    {
        if (newValue != null)
        {
            foreach (var item in (IEnumerable<VisualElement>)newValue)
                control.Add(item); 
        }
    }
    #endregion


    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Selected?.Invoke(this);
        IsVisible = !IsVisible;
        Arrow.Rotation = Arrow.Rotation == 180 ? 0 : 180; 
    }
}