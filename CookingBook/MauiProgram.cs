using CookingBook.Services;
using Microsoft.Extensions.Logging;

namespace CookingBook
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                  //  fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("BalsamiqSans-Italic.ttf", "BalsamiqItalic");
                });

            string dbPath = FileAccessHelper.GetLocationFilePath("recipe.db3");
            builder.Services.AddSingleton<RecipeRepository>(s => ActivatorUtilities.CreateInstance<RecipeRepository>(s, dbPath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
