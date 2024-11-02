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
            await DisplayAlert("Error ","Name is requred.End minimum 3 symbols maximun 50", "Ok");
            return;
        }

        if (textValidator.IsNotValid)
        {
            await DisplayAlert("Error ", "desctiprion is maximum 1000 sumbols", "Ok");
            return;
        }

        await App.RecipeRepo.AddRecipe(entryName.Text, entryDescr.Text);
        await Shell.Current.GoToAsync(nameof(ListPage));
		
    }
}