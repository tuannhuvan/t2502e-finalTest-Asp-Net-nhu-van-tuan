using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2502E_Comicsys.Models;

public class RentalDetail
{
    [Key]
    public int RentalDetailId { get; set; }

    [Required]
    public int RentalId { get; set; }

    [ForeignKey("RentalId")]
    public Rental Rental { get; set; }

    [Required]
    public int ComicBookId { get; set; }

    [ForeignKey("ComicBookId")]
    public ComicBook ComicBook { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required, Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
}