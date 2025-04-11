using SQLite;
using CookingBook.Models;
using CookingBook.Pages;




namespace CookingBook.Services
{
    public class RecipeRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly string _dbPath;

        public async Task Init()
        {
            try
            {
                await DatabaseHelper.CopyDatabaseIfNotExists(); // Копіюємо базу, якщо її немає
                await _connection.CreateTableAsync<Recipe>();   // Переконуємось, що таблиця існує
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
            }
        }

        public RecipeRepository(string dbPath)
        {
            _dbPath = dbPath;
            _connection = new SQLiteAsyncConnection(_dbPath);
        }


        public async Task<List<Recipe>> GetAllRecipe()
        {
            await Init();

            try
            {
                return await _connection.Table<Recipe>().ToListAsync();
            }
            catch (Exception ex)
            {
                // Логування помилки
                Console.WriteLine($"Error fetching recipes from database: {ex.Message}");
                return new List<Recipe>(); // Повертаємо порожній список в разі помилки
            }
        }

        public async Task AddRecipe(string name, string descr, string typeDish)
        {
            int result = 0;
            await Init();

            result = await _connection.InsertAsync(new Recipe { Name = name, Description = descr, TypeDish = typeDish  });

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
            // Перевірка відповідності ідентифікаторів
            if (recipeId != recipe.id)
            {
                throw new ArgumentException("Recipe id does not match provided id.");
            }

            // Знаходимо рецепт за його ідентифікатором
            var RecipeUpDate = await _connection.Table<Recipe>().FirstOrDefaultAsync(r => r.id == recipeId);

            if (RecipeUpDate != null)
            {
                // Оновлюємо значення полів
                RecipeUpDate.Name = recipe.Name;
                RecipeUpDate.Description = recipe.Description;
                RecipeUpDate.TypeDish = recipe.TypeDish;

                // Виконуємо оновлення в базі даних
                int rowsAffected = await _connection.UpdateAsync(RecipeUpDate);

                // Повертаємо оновлений об'єкт рецепта, якщо все пройшло успішно
                if (rowsAffected > 0)
                {
                    return RecipeUpDate;
                }
                else
                {
                    throw new Exception("Update failed: Recipe was not found or update operation failed.");
                }
            }
            else
            {
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
                await Shell.Current.GoToAsync(nameof(FirstPage));
            }
            else
            {
                var random = new Random();
                var randomIndex = random.Next(0, recipes.Count);
                return recipes[randomIndex];
            }
            return null;

        }

        public async Task<List<Recipe>> SearchDB(string TextSearch)
        {
            //old search
            // var ResSearch =  _connection.Table<Recipe>().Where(x => !string.IsNullOrEmpty(x.Name) && x.Name.StartsWith(TextSearch, StringComparison.OrdinalIgnoreCase))?.ToListAsync();

            if (string.IsNullOrEmpty(TextSearch))
                return new List<Recipe>();

            string searchPattern = $"%{TextSearch}%";

            // Використовуємо LINQ для пошуку з Contains у якості альтернативи
            var ResSearch = await _connection.Table<Recipe>()
                .Where(x => x.Name != null && x.Name.Contains(TextSearch))
                .ToListAsync();

            return ResSearch;

            
        }

        public async Task<List<Recipe>> FilerRecipe(string filterPick)
        {
            await Init();

            if (string.IsNullOrEmpty(filterPick))
                return new List<Recipe>();

            var filteredRecipe = await _connection.Table<Recipe>().Where(x => x.TypeDish == filterPick).ToListAsync();

            return filteredRecipe; 
        }
       



              
    }
}
