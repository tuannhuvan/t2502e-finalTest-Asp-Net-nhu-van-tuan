using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2502E_Comicsys.Models;

public class Rental
{
    [Key]
    public int RentalId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    [Required]
    public DateTime RentalDate { get; set; }

    [Required]
    public DateTime ReturnDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; } // Ví dụ: "Đang thuê", "Đã trả"

    // Navigation property
    public ICollection<RentalDetail> RentalDetails { get; set; }
}