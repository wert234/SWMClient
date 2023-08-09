
using System.Windows.Input;

namespace SWMClient.Infrastructure.Controls;

public partial class Expander : ContentView
{

    #region Events

    public event Action<object, TextChangedEventArgs> SelectedChanged;

    #endregion

    #region SelectedItemIndex

    public static readonly BindableProperty SelectedItemIndexProperty = BindableProperty.Create(
    nameof(SelectedItemIndex),
    typeof(int),
    typeof(Expander),
    default(int),
    BindingMode.TwoWay, propertyChanging: SelectedItemIndexPropertyChanged);

    private static void SelectedItemIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as Expander;
        control.SelectedItemIndex = (int)newValue;
    }

    public int SelectedItemIndex
    {
        get => (int)GetValue(SelectedItemIndexProperty);
        set => SetValue(SelectedItemIndexProperty, value);
    }

    #endregion

    #region Header 

    public static readonly BindableProperty HeaderProperty = BindableProperty.Create(
         nameof(Header),
         typeof(string),
         typeof(Expander),
         default(string),
         BindingMode.TwoWay,
        propertyChanged: HeaderPropertyChanged);

          private static void HeaderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
          {
              var control = bindable as Expander;
              control.Button.Text = (string)newValue;
          }

          public string Header
          {
              get => (string)GetValue(HeaderProperty);
              set => SetValue(HeaderProperty, value);
          }

    #endregion

    #region IsViseble

           public static readonly BindableProperty IsExpandedProperty = BindableProperty.Create(
           nameof(IsExpanded),
           typeof(bool),
           typeof(Expander),
           false,
           BindingMode.TwoWay,
           propertyChanged: IsExpandedPropertyChanged);
           
               private static void IsExpandedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
               {
                   var control = bindable as Expander;
                   control.Scroll.IsVisible = (bool)newValue;
               }
           
               public bool IsExpanded
               {
                   get => (bool)GetValue(IsExpandedProperty);
                   set => SetValue(IsExpandedProperty, value);
               }

    #endregion

    #region Items

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
    nameof(Items),
    typeof(IEnumerable<Models.JsonModels.DwarvecsTypes>),
    typeof(Expander),
    null,
    BindingMode.TwoWay,
    propertyChanged: ItemsPropertyChanged);

    private static void ItemsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = bindable as Expander;
        BindableLayout.SetItemsSource(control.Content, (IEnumerable<Models.JsonModels.DwarvecsTypes>)newValue);   
    }

    public IEnumerable<Models.JsonModels.DwarvecsTypes> Items
    {
        get => (IEnumerable<Models.JsonModels.DwarvecsTypes>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    #endregion

    public Expander()
    {
        InitializeComponent();
        Scroll.IsVisible = IsExpanded;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        IsExpanded = !IsExpanded;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = sender as Button;

        Header = button.Text;
        SelectedItemIndex = int.Parse(button.ClassId);
        SelectedChanged?.Invoke(sender, new TextChangedEventArgs("0", SelectedItemIndex.ToString()));
        IsExpanded = !IsExpanded;
    }

    private void Content_Loaded(object sender, EventArgs e)
    {
        if (Items.Count() > 0)
            Header = Items.First(x => x.id == SelectedItemIndex).name;
    }
}