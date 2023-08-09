using System.Text.Json;

namespace SWMClient.Infrastructure.Controls;

public partial class DataGridSnowWhiteItem : DataGridItem
{

    #region Fileds

    private Models.SWMClient _client;

    #endregion

    public DataGridSnowWhiteItem()
	{
		InitializeComponent();
        _client = new Models.SWMClient();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        
    }
}