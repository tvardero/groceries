using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Groceries.Models;

public class Product
{
    public uint Id { get; init; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public Category Category { get; set; } = null!;

    public string? Description { get; set; }

    [Required, Range(0d, (double)decimal.MaxValue)]
    [DataType(DataType.Currency)]
    [Precision(18, 2)]
    public decimal Price { get; set; }
}