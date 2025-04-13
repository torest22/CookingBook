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
                await DatabaseHelper.CopyDatabaseIfNotExists(); 
                await _connection.CreateTableAsync<Recipe>();           
        }

        public RecipeRepository(string dbPath)
        {
            _dbPath = dbPath;
            _connection = new SQLiteAsyncConnection(_dbPath);
        }

        public async Task<List<Recipe>> GetAllRecipe()
        {
            await Init();
            
            return await _connection.Table<Recipe>().ToListAsync();         
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

            var RecipeUpDate = await _connection.Table<Recipe>().FirstOrDefaultAsync(r => r.id == recipeId);

            if (RecipeUpDate != null)
            {              
                RecipeUpDate.Name = recipe.Name;
                RecipeUpDate.Description = recipe.Description;
                RecipeUpDate.TypeDish = recipe.TypeDish;
                
                int rowsAffected = await _connection.UpdateAsync(RecipeUpDate);

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
