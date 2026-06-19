using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2502E_Comicsys.Models;

public class RentalDetails
{
    [Key]
    public int RentalDetailId { get; set; }
    public int RentalId { get; set; }
    public int ComicBookId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải tối thiểu là 1.")]
    public int Quantity { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Giá mỗi ngày phải là một giá trị dương..")]
    public decimal PricePerDay { get; set; }
    
    [ForeignKey("RentalId")]
    public virtual Rentals? Rental { get; set; }

    [ForeignKey("ComicBookId")]
    public virtual ComicBooks? ComicBook { get; set; }
}