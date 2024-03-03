using artclub.Data;
using artclub.Models;

namespace artclub.Pages;

public partial class LotteryPage : ContentPage
{
    private readonly DbService _dbService;
    private Member _winnerMember=new Member();
    public LotteryPage(DbService dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        introLabel.Text=$"Welcome to the lottery! Today is {DateTime.Now.ToString("dd - MMM - yyyy")}";
        pickArtButton.IsVisible=false;
        absentButton.IsVisible=false;
        notInterestedButton.IsVisible=false;
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

        var members=await _dbService.GetMembersForDrawAsync(DateTime.Now.Year.ToString());
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
                _winnerMember=member;

                //Add the winner to the lottery draw table
                var lotteryDraw=new LotteryDraw
                {
                    DrawDate=DateTime.Now,
                    WinnerId=member.Id,
                    BatchId=DateTime.Now.Year.ToString()
                };
                await _dbService.CreateDrawAsync(lotteryDraw);

                pickArtButton.IsVisible = true;
                absentButton.IsVisible = true;
                notInterestedButton.IsVisible = true;
                return;
            }
        }
        



    }

    private async void absentButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Are you sure?", $"Are you sure {_winnerMember.Name} is absent today", "Yes", "No");
        if(answer)
        {
            var draw=await _dbService.GetDrawsAsync(_winnerMember.Id,DateTime.Now.Year.ToString());
            if(draw is not null)
            {
                draw.IsAbsent=true;
                await _dbService.UpdateDrawAsync(draw);
                pickArtButton.IsVisible = false;
                absentButton.IsVisible = false;
                notInterestedButton.IsVisible = false;
            }
        }
    }
    private async void pickArtButtonClicked(object sender, EventArgs e)
    {
        //Navigate to the SelectArtPage and pass the winner member object to the page
        await Navigation.PushAsync(new SelectArtPage(_dbService,_winnerMember));
    }
    private async void notInterestedButtonClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Are you sure?", $"Are you sure {_winnerMember.Name} is not interested to select any art", "Yes", "No");
        if (answer)
        {
            var draw = await _dbService.GetDrawsAsync(_winnerMember.Id, DateTime.Now.Year.ToString());
            if (draw is not null)
            {
                draw.IsNotInterested = true;
                await _dbService.UpdateDrawAsync(draw);
                pickArtButton.IsVisible = false;
                absentButton.IsVisible = false;
                notInterestedButton.IsVisible = false;
            }
        }
    }
}