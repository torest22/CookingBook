using SQLite;

namespace CookingBook.Models;

[Table("TableRecipe")]

public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public  int id { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
    
    public string Description { get; set; }
}
