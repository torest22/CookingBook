using SQLite;
using CookingBook.Models;
using CookingBook.Pages;




namespace CookingBook.Services
{
    public class RecipeRepository
    {
        string _dbPath;

        private SQLiteAsyncConnection _connection;


        public async Task Init()
        {

            _connection = new SQLiteAsyncConnection(_dbPath);

            await _connection.CreateTableAsync<Recipe>();

        }

        public RecipeRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<Recipe>> GetAllRecipe()
        {
            await Init();

            return await _connection.Table<Recipe>().ToListAsync();
        }

        public async Task AddRecipe(string name, string descr)
        {
            int result = 0;
            await Init();

            result = await _connection.InsertAsync(new Recipe { Name = name, Description = descr });

        }

        public async Task DeleteRecipe(int id)
        {
            await Init();
            await _connection.DeleteAsync<Recipe>(id);

        }



        public async Task<Recipe> GetByIdAsync(int id)
        {
            return await _connection.Table<Recipe>().FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task<Recipe> UpdateRecipe(int recipeId, Recipe recipe)
        {
            // Перевірка, чи збігається recipeId з id об'єкта recipe
            if (recipeId != recipe.id)
            {
                // Якщо id не збігаються, можна викинути помилку або взяти інші заходи
                throw new ArgumentException("Recipe id does not match provided id.");
            }

            // Знаходимо рецепт за його ідентифікатором у базі даних
            var RecipeUpDate = await _connection.Table<Recipe>().FirstOrDefaultAsync(r => r.id == recipeId);

            if (RecipeUpDate != null)
            {
                // Оновлюємо властивості рецепта з об'єкта recipe
                RecipeUpDate.Name = recipe.Name;
                RecipeUpDate.Description = recipe.Description;

                // Виконуємо оновлення у базі даних і отримуємо кількість оновлених записів
                int rowsAffected = await _connection.UpdateAsync(RecipeUpDate);

                // Якщо оновлення успішне, повертаємо оновлений об'єкт рецепта
                if (rowsAffected > 0)
                {
                    return RecipeUpDate;
                }
                else
                {
                    // Можна обробити ситуацію, коли нічого не оновлено
                    throw new Exception("Update failed: Recipe was not found or update operation failed.");
                }
            }
            else
            {
                // Можна обробити ситуацію, коли рецепт не знайдено
                throw new KeyNotFoundException($"Recipe with id {recipeId} not found.");
            }
        }
    

        public async Task<Recipe> GetRNDRecipe()
        {
            await Init();




            var recipes = await _connection.Table<Recipe>().ToListAsync();
            if (recipes.Count <= 0)
            {
               await Application.Current.MainPage.DisplayAlert("Attention", "You dont have recipie", "ОК");
                //  return null;
                await Shell.Current.GoToAsync(nameof(FistPage));
            }
            else
            {
                var random = new Random();
                var randomIndex = random.Next(0, recipes.Count);
                return recipes[randomIndex];
            }
            return null;

        }
    }
}
