namespace CookingBook.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
	}

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsNotValid)
        {
            await DisplayAlert("Error","Name is requred.End minimum 3 symbols maximun 30", "Ok");
            return;
        }


        await App.RecipeRepo.AddRecipe(entryName.Text, entryDescr.Text);
        await Shell.Current.GoToAsync(nameof(ListPage));
		
    }
}