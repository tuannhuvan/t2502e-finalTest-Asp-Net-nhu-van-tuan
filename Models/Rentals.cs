using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2502E_Comicsys.Models;

public class Rentals
{
    [Key]
    public int RentalID { get; set; }
    public int CustomerID { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; } = string.Empty; 
    [ForeignKey("CustomerID")]
    public Customers?  Customer { get; set; }  
}