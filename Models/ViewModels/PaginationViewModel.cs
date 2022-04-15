namespace Groceries.Models.ViewModels;

public class PaginationViewModel
{
    public uint CurrentPage { get; set; }

    public uint TotalItems { get; set; }

    public uint ItemsPerPage { get; set; }

    public uint TotalPages => (TotalItems + ItemsPerPage - 1) / ItemsPerPage;
}