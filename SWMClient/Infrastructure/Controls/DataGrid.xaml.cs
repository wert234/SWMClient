using SWMClient.Models;
using System.Collections.ObjectModel;

namespace SWMClient.Infrastructure.Controls;

public partial class DataGrid : ContentView
{

    #region Filed

        private static RowDefinitionCollection rowDefinitions;

        private static TapGestureRecognizer itemClike;

    #endregion

    #region Propertys

         #region Count

              private int count = 1;
              public int Count
              {
                  get => count;
                  set => count = value;
              }

         #endregion

         #region ColumnSpacing

              public static readonly BindableProperty ColumnSpacingProperty
              = BindableProperty.Create(nameof(ColumnSpacing), typeof(double), typeof(DataGrid));
              public double ColumnSpacing
              {
                  get => (double)GetValue(ColumnSpacingProperty);
                  set => SetValue(ColumnSpacingProperty, value);
              }

         #endregion

         #region  TitleItem

             public static readonly BindableProperty TitleItemProperty
             = BindableProperty.Create(nameof(TitleItem), typeof(VisualElement), typeof(DataGrid), null, BindingMode.TwoWay, propertyChanged: TitleItemPropertyChanged);
             private static void TitleItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
             {
                 var control = bindable as DataGrid;
                 if (control.Content.Count > 0)
                     control.Content[0] = (VisualElement)newValue;
             }
             public VisualElement TitleItem
             {
                 get => (VisualElement)GetValue(TitleItemProperty);
                 set => SetValue(TitleItemProperty, value);
             }

         #endregion

         #region Items

             public static readonly BindableProperty ItemsProperty
             = BindableProperty.Create(nameof(Items), typeof(ObservableCollection<DataGridItem>), typeof(DataGrid), null, BindingMode.TwoWay, propertyChanged: ItemsPropertyChange);
             private static void ItemsPropertyChange(BindableObject bindable, object oldValue, object newValue)
              {
                  var control = (DataGrid)bindable;

                  control.Count = 1;
                  rowDefinitions.Clear();
                  rowDefinitions.Add(new RowDefinition(new GridLength(50)));

                  foreach (DataGridItem item in (ObservableCollection<DataGridItem>)newValue)
                  {
                      rowDefinitions.Add(new RowDefinition(new GridLength(50)));

                      item.ZIndex = -control.Count;
                      item.GestureRecognizers.Add(itemClike);
                      control.Content.Add(item);
                      Grid.SetRow(item, control.Count);
                      control.Count++;
                  }
              }
             public ObservableCollection<DataGridItem> Items
             {
                 get => (ObservableCollection<DataGridItem>)GetValue(ItemsProperty);
                 set => SetValue(ItemsProperty, value);
             }

    #endregion

         #region SelectedItemIndex

             public static readonly BindableProperty SelectedItemIndexProperty
             = BindableProperty.Create(nameof(SelectedItemIndex), typeof(int), typeof(DataGrid), null, BindingMode.TwoWay);
             public int SelectedItemIndex
             {
                 get => (int)GetValue(SelectedItemIndexProperty);
                 set => SetValue(SelectedItemIndexProperty, value);
             }

    #endregion

    #endregion

    #region Init
    public DataGrid()
    {
        InitializeComponent();
        rowDefinitions = new RowDefinitionCollection(new RowDefinition(new GridLength(50)));
        Content.RowDefinitions = rowDefinitions;
        
        itemClike = new TapGestureRecognizer();
        itemClike.Tapped += ItemClike_Tapped;
    }

    private void ContentView_Loaded(object sender, EventArgs e)
    {
        Content.Add(TitleItem);
    }

    private void ItemClike_Tapped(object sender, TappedEventArgs e)
    {
        var item = (DataGridItem)sender;
        SelectedItemIndex = item.Id;
    }

    #endregion
}