using CookingBook.Services;
using CookingBook.Models;
using System.Diagnostics;

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
        // Встановлюємо ItemsSource для Picker
        TypeDishFilter.ItemsSource = new List<string> { "1st dish", "2nd dish","Salad", "Desert", "Drink", "Snack" };

        // Додаємо обробник події SelectedIndexChanged
        TypeDishFilter.SelectedIndexChanged += (s, e) =>
        {
            if (TypeDishFilter.SelectedItem != null)
            {
                _recipe.TypeDish = TypeDishFilter.SelectedItem.ToString();
            }
        };

        // Завантажуємо рецепт з бази даних
        _recipe = await App.RecipeRepo.GetByIdAsync(recipeId);
        if (_recipe != null)
        {
            entryName.Text = _recipe.Name;
            entryDescr.Text = _recipe.Description;

            // Затримка для впевненості, що Picker готовий до прийняття значення
            await Task.Delay(50);

            // Встановлюємо значення для Picker (перевіряємо, чи є в ItemsSource)
            if (TypeDishFilter.ItemsSource.Contains(_recipe.TypeDish))
            {
                TypeDishFilter.SelectedItem = _recipe.TypeDish;
            }
            else
            {
                Console.WriteLine($"Значення {_recipe.TypeDish} не знайдено в ItemsSource Picker");
            }
        }
    }




    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (_recipe != null)
        {
            bool answer = await DisplayAlert("Question?", "Are you sure you want to DELETE recipe?", "Yes", "No");
            Debug.WriteLine("Answer: "+ answer);
            if (answer is true)
            {
                await App.RecipeRepo.DeleteRecipe(_recipe.id);
                await Shell.Current.GoToAsync(nameof(ListPage));
            }

            

        }
    }

    private async void btnUpDate_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsNotValid)
        {
            await DisplayAlert("Error ", "Name is requred.End minimum 3 symbols maximun 50", "Ok");
            return;
        }
        if (textValidator.IsNotValid)
        {
            await DisplayAlert("Error ", "desctiprion is maximum 1000 sumbols", "Ok");
            return;
        }

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
    }

    private async void Btnback_Clicked(object sender, EventArgs e)
    {
        if (_recipe != null)
        {
            bool answer = await DisplayAlert("Question:", "Do you want exit without change?", "Yes", "No");
            Debug.WriteLine("Answer: " + answer);
            if (answer is true)
            {
             
                await Shell.Current.GoToAsync(nameof(ListPage));
            }



        }
    }
}
