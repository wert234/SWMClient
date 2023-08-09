

namespace SWMClient.Infrastructure.Controls;

public partial class DataGridDwarvesItem : DataGridItem
{
    private Models.SWMClient _client;

    public override void ContentView_Loaded(object sender, EventArgs e)
    {
        _client = new Models.SWMClient();
        ((Entry)Items.ToArray()[0]).TextChanged += Entry_TextChanged;
        ((Expander)Items.ToArray()[1]).SelectedChanged += Entry_TextChanged;

        DataGridItemInit();
    }
    public DataGridDwarvesItem()
	{
		InitializeComponent();
        _client = new Models.SWMClient();
	}
    private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_client != null)
        {
            await _client.EditDwarfsAsync(((Entry)Items.ToArray()[0]).Text, ((Expander)Items.ToArray()[1]).SelectedItemIndex, Id);
        }
    }
}