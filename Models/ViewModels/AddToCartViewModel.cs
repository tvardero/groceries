using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Groceries.Models.ViewModels;

public class AddToCartViewModel
{
    [HiddenInput]
    public uint ProductId { get; init; }

    [Required, Range(0, 1000)]
    public uint Quantity { get; init; } = 0;
}