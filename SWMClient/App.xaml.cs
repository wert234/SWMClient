namespace SWMClient;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}

    public override void CloseWindow(Window window)
    {
         if(Preferences.Get("isRemember", false) is false)
            Preferences.Clear();
    }

    protected override void OnStart()
    {
        if (Preferences.Get("isRemember", false) is true)
            Shell.Current.GoToAsync("//MainPage");
    }
}
