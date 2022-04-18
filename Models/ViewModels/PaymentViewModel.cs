using Microsoft.AspNetCore.Mvc;

namespace Groceries.Models.ViewModels;

public class PaymentViewModel
{
    [HiddenInput]
    public uint OrderId { get; set; }
}