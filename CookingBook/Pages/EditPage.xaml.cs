using CookingBook.Services;
using CookingBook.Models;

namespace CookingBook.Pages;

public partial class EditPage : ContentPage
{

    private RecipeRepository _database;
    private Recipe _recipe;
    private int recId;
    private Recipe rndID;

    public EditPage(int recipeId)
    {
        InitializeComponent();

        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recipes.db");
        _database = new RecipeRepository(dbPath);

        LoadRecipe(recipeId);

        recId = recipeId;
    }

    public EditPage(Recipe rndID)
    {
        this.rndID = rndID;
    }

    private async void LoadRecipe(int recipeId)
    {
        _recipe = await App.RecipeRepo.GetByIdAsync(recipeId);
        if (_recipe != null)
        {
            entryName.Text = _recipe.Name;
            entryDescr.Text = _recipe.Description;
        }
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (_recipe != null)
        {
            await App.RecipeRepo.DeleteRecipe(_recipe.id);
            await DisplayAlert("Success", "Recipe deleted successfully.", "OK");

            await Shell.Current.GoToAsync(nameof(ListPage));

        }
    }

    private async void btnUpDate_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Оновлюємо рецепт за допомогою методу з репозиторію
            _recipe = await App.RecipeRepo.UpdateRecipe(recId, _recipe);

            if (_recipe != null)
            {
                // Оновлюємо дані рецепта зі значеннями з полів введення
                _recipe.Name = entryName.Text;
                _recipe.Description = entryDescr.Text;

                // Фактично оновлюємо рецепт у базі даних після оновлення властивостей
                await App.RecipeRepo.UpdateRecipe(recId, _recipe);

                // Повідомляємо про успішне оновлення
                await DisplayAlert("Success", "Recipe updated successfully.", "OK");

                // Переходимо на сторінку зі списком після успішного оновлення
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
            else
            {
                // Можна обробити ситуацію, коли оновлення не було успішним
                await DisplayAlert("Error", "Failed to update recipe.", "OK");
            }
        }
        catch (Exception ex)
        {
            // Обробляємо будь-які винятки, що можуть виникнути під час оновлення
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
