using CookingBook.Services;

namespace CookingBook.Pages;

public partial class FistPage : ContentPage
{
	public FistPage()
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