using CookingBook.Services;

namespace CookingBook.Pages;

public partial class FirstPage : ContentPage
{
	public FirstPage()
	{
		InitializeComponent();
	}
    private  async void btnRecipe_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(nameof(ListPage));
    }

    private async void btnRandom_Clicked(object sender, EventArgs e)
    {
       await  Shell.Current.GoToAsync(nameof(RandomPage));
    }

   
}