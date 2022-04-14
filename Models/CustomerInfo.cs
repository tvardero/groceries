using System.ComponentModel.DataAnnotations;

namespace Groceries.Models;

public class CustomerInfo
{
    [Required(ErrorMessage = "Потрібно вказати ім'я"), MaxLength(50)]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати призвище"), MaxLength(50)]
    [Display(Name = "Призвище")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати номер телефону"), MaxLength(9)]
    [Display(Name = "Номер телефону")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати e-mail"), MaxLength(100)]
    [Display(Name = "E-mail")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати область"), MaxLength(50)]
    [Display(Name = "Область")]
    public string AddressRegion { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати населений пункт"), MaxLength(50)]
    [Display(Name = "Населений пункт")]
    public string AddressCity { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати вулицю"), MaxLength(50)]
    [Display(Name = "Вулиця")]
    public string AddressStreet { get; set; } = null!;

    [Required(ErrorMessage = "Потрібно вказати номер дому"), MaxLength(50)]
    [Display(Name = "Номер дому")]
    public string AddressHouse { get; set; } = null!;

    [MaxLength(50)]
    [Display(Name = "Квартира")]
    public string? AddressFlat { get; set; }

    [MaxLength(50)]
    [Display(Name = "Поштовий індекс")]
    [DataType(DataType.PostalCode)]
    public string? AddressPostalCode { get; set; }
}