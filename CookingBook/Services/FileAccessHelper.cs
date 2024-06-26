namespace CookingBook.Services
{
    public class FileAccessHelper
    {
        public static string GetLocationFilePath(string filePath)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filePath);
        }
    }
}
