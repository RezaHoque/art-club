using artclub.Data;

namespace artclub.Pages;

public partial class About : ContentPage
{
    private readonly DbService _dbService;
    public About(DbService dbService)
	{
		_dbService = dbService;
		
		InitializeComponent();
        var appVersion = AppInfo.Current.Version;

		appVersionLabel.Text = $"App Version: {appVersion}";

		var author = "Rezaul Hoque";
		var github = "https://github.com/RezaHoque/art-club";
		var email = "reza.hoque@outlook.com";
		authorLabel.Text = $"Author: {author} Email: {email} \r\nRepo: {github}";
    }
}