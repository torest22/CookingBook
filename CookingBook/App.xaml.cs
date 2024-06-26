using CookingBook.Services;

namespace CookingBook
{
    public partial class App : Application
    {
        public static RecipeRepository RecipeRepo { get;private set; }
        public App(RecipeRepository repo)
        {
            InitializeComponent();

            MainPage = new AppShell();

            RecipeRepo = repo;
        }
    }
}
