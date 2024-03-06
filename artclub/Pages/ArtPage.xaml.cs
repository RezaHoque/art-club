using artclub.Data;
using artclub.Models;
using CommunityToolkit.Maui.Storage;

namespace artclub.Pages;

public partial class ArtPage : ContentPage
{
    private readonly DbService _dbService;
    private int _editArtId;
    private IFileSaver _fileSaver;
    public ArtPage(DbService dbService,IFileSaver fileSaver)
    {
        InitializeComponent();
        _dbService = dbService;
        _fileSaver = fileSaver;
        Task.Run(async () => artCollection.ItemsSource = await _dbService.GetArtsAsync());
    }
    private async void saveButton_Clicked(object sender, EventArgs e)
    {
        if(_editArtId == 0)
        {
            //if (string.IsNullOrEmpty(priceEntryField.Text) || string.IsNullOrEmpty(artistEntryField.Text))
            //{
            //    await DisplayAlert("Error", "Price and Artist are required", "Ok");
            //    return;
            //}
            await _dbService.CreateArtAsync(new Models.Art
            {
                Title = titleEntryField.Text,
                Artist = artistEntryField.Text,
                ImageUrl = filePathLabel.Text,
                Price = priceEntryField.Text,
                Size = sizeEntryField.Text,
                Description = descriptionEntryField.Text,
                Batch = batchEntryField.Text,
                ActionDate = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null)
            });
        }
        else
        {
            await _dbService.UpdateArtAsync(new Models.Art
            {
                Id = _editArtId,
                Title = titleEntryField.Text,
                Artist = artistEntryField.Text,
                ImageUrl = filePathLabel.Text,
                Price = priceEntryField.Text,
                Size = sizeEntryField.Text,
                Description = descriptionEntryField.Text,
                Batch = batchEntryField.Text,
                ActionDate = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", null)
            });
        }
        
        _editArtId = 0;
        titleEntryField.Text = string.Empty;
        artistEntryField.Text = string.Empty;
        priceEntryField.Text = string.Empty;
        sizeEntryField.Text = string.Empty;
        descriptionEntryField.Text = string.Empty;
        batchEntryField.Text = string.Empty;
        filePathLabel.Text = string.Empty;
        imagePreview.Source = null;
        imagePreview.IsVisible = false;
        artCollection.ItemsSource = await _dbService.GetArtsAsync();

    }
    private async void filePickerButton_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Please select an image"
        });
        if (result != null)
        {
            var stream = await result.OpenReadAsync();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes.AsMemory(0, (int)stream.Length));
            imagePreview.HeightRequest = 300;
            imagePreview.WidthRequest = 300;
            imagePreview.Margin = new Thickness(3, 3, 3, 3);
            imagePreview.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            imagePreview.IsVisible = true;

            filePathLabel.Text = result.FullPath;
        }
    }

    private async void artCollection_ItemTapped(object sender, EventArgs e)
    {
        //var art = e.Item as Models.Art;
        var image = (Image)sender;
        var art = (Art)image.BindingContext;
        var action = await DisplayActionSheet("Actions", "Cancel", null, "Edit", "Delete");
        switch (action)
        {
            case "Edit":
                _editArtId = art.Id;
                titleEntryField.Text = art.Title;
                artistEntryField.Text = art.Artist;
                priceEntryField.Text = art.Price;
                sizeEntryField.Text = art.Size;
                descriptionEntryField.Text = art.Description;
                batchEntryField.Text = art.Batch;
                filePathLabel.Text = art.ImageUrl;
                imagePreview.Source = ImageSource.FromFile(art.ImageUrl);
                imagePreview.IsVisible = true;

                break;
            case "Delete":
                await _dbService.DeleteArtAsync(art);
                artCollection.ItemsSource = await _dbService.GetMembersAsync();
                break;
        }

    }
}