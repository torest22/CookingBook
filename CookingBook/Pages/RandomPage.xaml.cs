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
        LoadTypeRecipe();

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");
        using (var db = new SQLite.SQLiteConnection(dbPath)) ;


    }

    private async void btnAllRandom_Clicked(object sender, EventArgs e)
    {

        var recipe = await App.RecipeRepo.GetRNDRecipe();

       
        if (recipe != null)
        {
            int rndID = recipe.id;
            await Navigation.PushAsync(new ViewPage(rndID));
        }
        else
        {
            await Shell.Current.GoToAsync(nameof(FistPage));
        }
    }

    private async void LoadTypeRecipe()
    {
        TypeDishFilter.ItemsSource = new List<string> { "1st dish", "2nd dish", "Desert", "Drink", "Snack" };
    }

    private async void btnRndTypeDish_Clicked(object sender, EventArgs e)
    {
        var selectedType = TypeDishFilter.SelectedItem as string;


        List<Recipe> filterRecipe = await App.RecipeRepo.FilerRecipe(selectedType);



        if (filterRecipe != null && filterRecipe.Count > 0)
        {
            // Вибираємо випадковий рецепт з фільтрованого списку
            Random rand = new Random();
            Recipe randomRecipe = filterRecipe[rand.Next(filterRecipe.Count)];

            int rndID = randomRecipe.id; // Отримуємо id випадкового рецепта
            await Navigation.PushAsync(new ViewPage(rndID));
        }
        else
        {
            // Якщо не знайдено рецептів для вибраного типу, перенаправляємо на іншу сторінку
            await Shell.Current.GoToAsync(nameof(FistPage));
        }
    }
    }
