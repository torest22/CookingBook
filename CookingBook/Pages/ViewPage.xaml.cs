using CookingBook.Models;
using CookingBook.Services;
namespace CookingBook.Pages;

public partial class ViewPage : ContentPage
{
    private RecipeRepository  _database;
    private Recipe _recipe;
    private int recId;
    private Recipe randomRecipe;

    public ViewPage(int recipeId)
	{
		InitializeComponent();
        recId = recipeId;

        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");
        _database = new RecipeRepository(dbPath);

        LoadRecipe(recipeId);
    }

    public ViewPage(Recipe randomRecipe)
    {
        this.randomRecipe = randomRecipe;
    }

    private void BtnGoBack_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(ListPage));
    }

    private async void BtnGoToEdit_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditPage(recId));
    }

    private async void LoadRecipe(int recipeId)
    {
        _recipe = await App.RecipeRepo.GetByIdAsync(recipeId);
        if (_recipe != null)
        {
            labelName.Text = _recipe.Name;
            lableDescr.Text = _recipe.Description;
        }
    }
}