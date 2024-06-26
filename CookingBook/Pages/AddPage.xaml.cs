namespace CookingBook.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
		await App.RecipeRepo.AddRecipe(entyName.Text, entryDesr.Text);
        await Shell.Current.GoToAsync(nameof(ListPage));
		
    }
}