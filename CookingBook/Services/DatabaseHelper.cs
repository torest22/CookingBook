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
     
        if (!File.Exists(dbPath))
        {          
            using var stream = await FileSystem.OpenAppPackageFileAsync("recipe.db3");
            using var fileStream = File.Create(dbPath);
            await stream.CopyToAsync(fileStream);      
        }
        else
        {
         
        }
    }
}
