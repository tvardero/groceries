using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Groceries.Models;

public class Category
{
    [HiddenInput]
    public uint Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public List<Product> Products { get; } = new();
}