using artclub.Data;

namespace artclub.Pages;

public partial class MemberPage : ContentPage
{
	private readonly DbService _dbService;
	private int _editMemberId;
	public MemberPage(DbService dbService)
	{
		
		InitializeComponent();
        _dbService = dbService;
		Task.Run(async () => memberListview.ItemsSource = await _dbService.GetMembersAsync());
    }
	private async void saveButton_Clicked(object sender, EventArgs e)
	{
        
        if (_editMemberId == 0)
		{
          await _dbService.Create(new Models.Member
          {
              Name = nameEntryField.Text,
              Email = emailEntryField.Text,
              Phone = phoneEntryField.Text
          });
        }
        else
        {
            
            await _dbService.Update(new Models.Member
            {
                Id = _editMemberId,
                Name = nameEntryField.Text,
                Email = emailEntryField.Text,
                Phone = phoneEntryField.Text
            });
        }
       
        _editMemberId = 0;
        nameEntryField.Text = string.Empty;
        emailEntryField.Text = string.Empty;
        phoneEntryField.Text = string.Empty;

        memberListview.ItemsSource = await _dbService.GetMembersAsync();
    }
    private async void memberListview_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var member = e.Item as Models.Member;
        var action = await DisplayActionSheet("Actions", "Cancel", null, "Edit", "Delete");
        switch (action)
        {
            case "Edit":
                _editMemberId = member.Id;
                nameEntryField.Text = member.Name;
                emailEntryField.Text = member.Email;
                phoneEntryField.Text = member.Phone;
                break;
            case "Delete":
                await _dbService.Delete(member);
                memberListview.ItemsSource = await _dbService.GetMembersAsync();
                break;
        }
       
    }
}