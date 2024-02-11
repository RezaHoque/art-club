using artclub.Data;

namespace artclub.Pages;

public partial class HomePage : ContentPage
{
    private readonly DbService _dbService;
    public HomePage()
	{
		InitializeComponent();
		_dbService = new DbService();
	}
	async void Start_Clicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MemberPage(_dbService));
    }
}