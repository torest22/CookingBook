using CookingBook.Pages;
using CookingBook.Services;

namespace CookingBook
{
    public partial class App : Application
    {
        public static RecipeRepository RecipeRepo { get;private set; }
        public App(RecipeRepository repo)
        {
            InitializeComponent();
            RecipeRepo = repo;

            Task.Run(async () => await DatabaseHelper.CopyDatabaseIfNotExists());

            MainPage = new AppShell(); // Використовуємо тільки AppShell!
        }

    }
}
