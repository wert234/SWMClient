namespace SWMClient.Infrastructure.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AvtorizeEntry : ContentView
{
    #region Propertys

    #region EntryText

    public static readonly BindableProperty EntryTextProperty =
                     BindableProperty.Create(nameof(EntryText),
                         typeof(string),
                         typeof(AvtorizeEntry),
                         null,
                         BindingMode.TwoWay,
                         propertyChanged: OnEntryTextPropertyChanged);

    private void TextEntryControl_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        EntryText = entry.Text;
    }

    private static void OnEntryTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AvtorizeEntry)bindable;
        control.TextEntryControl.Text = (string)newValue;
    }

    public string EntryText
    {
        get => (string)GetValue(EntryTextProperty);
        set => SetValue(EntryTextProperty, value);
    }

    #endregion

    #region ImageEntry

    public static readonly BindableProperty ImageEntryProperty =
                     BindableProperty.Create(nameof(ImageEntry),
                         typeof(ImageSource),
                         typeof(AvtorizeEntry),
                         null,
                         BindingMode.TwoWay,
                         propertyChanged: OnImageEntryPropertyChanged);

    private static void OnImageEntryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AvtorizeEntry)bindable;
        control.ImageEntryControl.Source = (ImageSource)newValue;
    }

    public ImageSource ImageEntry
    {
        get => (ImageSource)GetValue(ImageEntryProperty);
        set => SetValue(ImageEntryProperty, value);
    }

    #endregion

    #region IsPassword

    private static readonly BindableProperty IsPasswordProperty
        = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(AvtorizeEntry));

    public bool IsPassword
    {
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    #endregion

    #endregion

    #region Init

    public AvtorizeEntry()
    {
        InitializeComponent();
        TextEntryControl.IsPassword = IsPassword;
    }

    #endregion

}