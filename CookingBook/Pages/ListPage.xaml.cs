﻿using CookingBook.Models;
namespace CookingBook.Pages;

public partial class ListPage : ContentPage
{
    private int recipeId;

    public ListPage()
	{
		InitializeComponent();
        LoadRecipe();
	}

    private void btnAddRecipe_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddPage));
    }

    private async void LoadRecipe()
    {
        List<Recipe> recipes = await App.RecipeRepo.GetAllRecipe();
        RecipeList.ItemsSource = recipes;
        
    }

    

    private  async void RecipeList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (e.SelectedItem is Recipe selectedRecipe)
        {
            int recipeId = selectedRecipe.id;            
            await Navigation.PushAsync(new ViewPage(recipeId));

            ((ListView)sender).SelectedItem = null;

        }

    }

    private async void btnSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
     List<Recipe> recipes = await App.RecipeRepo.SearchDB(((SearchBar)sender).Text);

        RecipeList.ItemsSource = recipes;       
    }
}