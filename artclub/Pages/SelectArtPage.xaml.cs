using artclub.Data;
using artclub.Models;
using Microsoft.VisualBasic;

namespace artclub.Pages;

public partial class SelectArtPage : ContentPage
{
	private readonly DbService _dbService;
	private Member _winner;
	public SelectArtPage(DbService dbService,Member winner)
	{
		InitializeComponent();
		_dbService = dbService;
		_winner=winner;
		winnerLabel.Text = $"Select Art for {_winner.Name}";

        Task.Run(async () => artCollectionView.ItemsSource = await _dbService.GetArtsForSelectionAsync());
    }
    private async void art_Tapped(object sender, EventArgs e)
    {
        if (sender is Image tappedImage)
        {
            var confirmation = await DisplayAlert("Confirmation", "Are you sure that you want to select this Art?", "Yes", "No");
            if (confirmation)
            {
                // Save to database
                var selectedArtObject = tappedImage.BindingContext as Art;
                if (selectedArtObject != null)
                {
                    var draw = await _dbService.GetDrawsAsync(_winner.Id, "2024");
                    if(draw is not null)
                    {
                        draw.ArtId = selectedArtObject.Id;
                        draw.PickedArt = true;

                        await _dbService.UpdateDrawAsync(draw);
                    }
                    
                    await DisplayAlert("Success", "Art object saved to database!", "OK");
                    await Navigation.PushAsync(new LotteryPage(_dbService));
                }
            }
        }
    }

}