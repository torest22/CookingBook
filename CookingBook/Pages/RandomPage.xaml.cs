using CookingBook.Models;
using SQLite;
namespace CookingBook.Pages;

public partial class RandomPage : ContentPage
{
    string _dbPath;
    private readonly string _connectionString;

    private SQLiteAsyncConnection _connection;
    public RandomPage()
    {
        InitializeComponent();

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");
        using (var db = new SQLite.SQLiteConnection(dbPath)) ;


    }

    private async void btnAllRandom_Clicked(object sender, EventArgs e)
    {

        var recipe = await App.RecipeRepo.GetRNDRecipe();

       
        if (recipe != null)
        {
            // Якщо є рецепт, переходимо на сторінку перегляду
            int rndID = recipe.id;
            await Navigation.PushAsync(new ViewPage(rndID));
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(FistPage));
        }
    }
    
}