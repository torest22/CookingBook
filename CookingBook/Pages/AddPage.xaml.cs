using System.Diagnostics;

namespace CookingBook.Pages;

public partial class AddPage : ContentPage
{
	public AddPage()
	{
		InitializeComponent();
        LoadTypeRecipe();
    }

    private async void LoadTypeRecipe()
    {
        TypeDishFilter.ItemsSource = new List<string> { "1st dish", "2nd dish", "Desert", "Drink", "Snack" };         
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
            await DisplayAlert("Error ", "desctiprion is maximum 5000 sumbols", "Ok");
            return;
        }

        string selectedTypeDish = TypeDishFilter.SelectedItem != null ? TypeDishFilter.SelectedItem.ToString() : ""; // AI
        await App.RecipeRepo.AddRecipe(entryName.Text, entryDescr.Text , selectedTypeDish);

        await Shell.Current.GoToAsync(nameof(ListPage));
		
    }

    private async void btnBack_Clicked(object sender, EventArgs e)
    {
        
            bool answer = await DisplayAlert("Question:", "Do you want exit without change?", "Yes", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer is true)
            {

                await Shell.Current.GoToAsync(nameof(ListPage));
            }
        
    }
}