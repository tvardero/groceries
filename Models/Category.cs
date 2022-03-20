using System.ComponentModel.DataAnnotations;

namespace Groceries.Models;

public class Category
{
    public uint Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public List<Product> Products { get; } = new();
}