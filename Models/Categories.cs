namespace Mission06_Trotter.Models;
using System.ComponentModel.DataAnnotations;

public class Categories
{
    [Key]
    public int CategoryId { get; set; }
    
    public string CategoryName { get; set; } = string.Empty;
}
