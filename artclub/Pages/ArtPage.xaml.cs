using artclub.Data;
using artclub.Models;

namespace artclub.Pages;

public partial class ArtPage : ContentPage
{
    private readonly DbService _dbService;
    public ArtPage(DbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
       // Task.Run(async () => artDataGrid.ItemsSource = await _dbService.GetMembersAsync());
    }
}