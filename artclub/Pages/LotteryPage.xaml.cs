using artclub.Data;

namespace artclub.Pages;

public partial class LotteryPage : ContentPage
{
    private readonly DbService _dbService;
    public LotteryPage(DbService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        introLabel.Text=$"Welcome to the lottery! Today is {DateTime.Now.ToString("dd - MMM - yyyy")}";
	}
    //Randomly select a winner from the members list. Each member object has a TotalLots property that represents the number of lottery tickets they have.
    //The probability of a member being selected is proportional to the number of lottery tickets they have.
    //For example, if member A has 2 lottery tickets and member B has 1 lottery ticket, member A should have more chance of being selected than member B.
    //The selected member should be displayed in the winnerLabel.
    //If there are no members in the list, display an error message in the winnerLabel.
    //If the draw button is clicked again, the winnerLabel should be cleared.
    private async void drawButtonClicked(object sender, EventArgs e)
	{
        winnerLabel.Text = string.Empty;
        lotterySpinner.IsRunning=true;
        lotterySpinner.IsVisible=true;
        //lotterySpinner.Color= Color.FromRgba(0,0,0,0.5);

        var members=await _dbService.GetMembersAsync();
        //delay for 5 seconds
        await Task.Delay(5000);
        if(members.Count==0)
        {
            winnerLabel.Text="No members to draw";
            return;
        }
        var totalLots=members.Sum(m=>m.TotalLots);
        var random=new Random();
        var randomNumber=random.Next(0,totalLots);
        var currentSum=0;
        foreach(var member in members)
        {
            currentSum+=member.TotalLots;
            if(currentSum>=randomNumber)
            {
                lotterySpinner.IsRunning=false;
                lotterySpinner.IsVisible=false;
                winnerLabel.Text=member.Name;
                return;
            }
        }
        



    }
}