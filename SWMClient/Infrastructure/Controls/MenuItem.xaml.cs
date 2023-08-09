using CommunityToolkit.Mvvm.Input;
using SWMClient.Views;
using System.Windows.Input;

namespace SWMClient.Infrastructure.Controls;

public partial class MenuItem : ContentView
{
    public MenuItem()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty CommandProperty
    = BindableProperty.Create(nameof(Command), typeof(Command), typeof(ContentView));

    public Command Command
    {
        get => (Command)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandParametrProperty
= BindableProperty.Create(nameof(CommandParametr), typeof(object), typeof(ContentView));

    public object CommandParametr
    {
        get => GetValue(CommandParametrProperty);
        set => SetValue(CommandParametrProperty, value);
    }

    public static readonly BindableProperty ItemTextProperty =
		BindableProperty.Create("ItemText", typeof(string), typeof(MenuItem));
    public string ItemText
    { 
		get => GetValue(ItemTextProperty) as string;
		set => SetValue(ItemTextProperty, value);
	}

    public static readonly BindableProperty ItemImageStartProperty =
    BindableProperty.Create("ItemImageStart", typeof(ImageSource), typeof(MenuItem));
    public ImageSource ItemImageStart
    {
        get => GetValue(ItemImageStartProperty) as ImageSource;
        set => SetValue(ItemImageStartProperty, value);
    }

    public static readonly BindableProperty ItemImageEndProperty =
    BindableProperty.Create("ItemImageEnd", typeof(ImageSource), typeof(MenuItem));
    public ImageSource ItemImageEnd
    {
        get => GetValue(ItemImageEndProperty) as ImageSource;
        set => SetValue(ItemImageEndProperty, value);
    }

    private void MenuItemControl_Loaded(object sender, EventArgs e)
    {
        TextControl.Text = ItemText;
        ImageStartControl.Source = ItemImageStart;
        ImageEndControl.Source = ItemImageEnd;
        MenuItemControl.GestureRecognizers.Add(new TapGestureRecognizer() { Command = Command, CommandParameter = CommandParametr });
    }
}