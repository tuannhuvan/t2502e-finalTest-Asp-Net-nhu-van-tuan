using System.ComponentModel.DataAnnotations;

namespace T2502E_Comicsys.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required, StringLength(255)]
    public string FullName { get; set; }

    [Required, StringLength(15)]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime RegisterDate { get; set; }

    // Navigation property
    public ICollection<Rental> Rentals { get; set; }
}