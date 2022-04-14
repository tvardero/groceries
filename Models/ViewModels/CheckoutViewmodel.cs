using Microsoft.AspNetCore.Mvc;

namespace Groceries.Models.ViewModels;

public class CheckoutViewModel
{
    public CustomerInfo CustomerInfo { get; set; } = new();

    [HiddenInput]
    public string? ReturnUrl { get; set; }
}