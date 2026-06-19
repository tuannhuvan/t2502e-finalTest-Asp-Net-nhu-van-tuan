using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace T2502E_Comicsys.Models;

public class Customers
{
    [Key]
    public int CustomerID { get; set; } 
    [StringLength(255)]
    public string FullName { get; set; } = string.Empty;
    [StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime RegistationDate { get; set; }

    // Navigation property
    public virtual ICollection<Rentals> Rentals { get; set; } = new Collection<Rentals>();

}