using artclub.Data;

namespace artclub.Pages;

public partial class LotteryResultPage : ContentPage
{
	private readonly DbService _dbService;
	public LotteryResultPage(DbService dbService)
	{
		InitializeComponent();
		_dbService = dbService;
		Task.Run(async () => winnerListView.ItemsSource = await _dbService.GetWinnersAsync());
	}
}