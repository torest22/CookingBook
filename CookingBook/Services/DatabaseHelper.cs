using System.IO;
using Microsoft.Maui.Storage;
using System.Threading.Tasks;

public static class DatabaseHelper
{
    public static string GetDatabasePath()
    {
        string filename = "recipe.db3";
        string folderPath = FileSystem.AppDataDirectory;
        return Path.Combine(folderPath, filename);
    }

    public static async Task CopyDatabaseIfNotExists()
    {
        string dbPath = GetDatabasePath();

        // Перевіряємо, чи база вже існує
        if (!File.Exists(dbPath))
        {
            // Якщо база не існує, копіюємо її з ресурсів
            using var stream = await FileSystem.OpenAppPackageFileAsync("recipe.db3");
            using var fileStream = File.Create(dbPath);
            await stream.CopyToAsync(fileStream);

            Console.WriteLine("Database copied successfully.");
        }
        else
        {
            Console.WriteLine("Database already exists.");
        }
    }
}
