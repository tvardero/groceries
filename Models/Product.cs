using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.Models;

public class Product
{
    [HiddenInput]
    public uint Id { get; init; }

    [Required, MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public Category Category { get; set; } = null!;

    [MaxLength(2500)]
    public string? Description { get; set; }

    [Required, Range(0d, (double)decimal.MaxValue)]
    [DataType(DataType.Currency), Precision(18, 2)]
    public decimal Price { get; set; }
}