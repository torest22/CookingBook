namespace CookingBook.Pages;

public partial class FistPage : ContentPage
{
	public FistPage()
	{
		InitializeComponent();
	}

    private void btnRecipe_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ListPage));
    }

    private void btnRandom_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(RandomPage));
    }

   
}