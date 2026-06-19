using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace T2502E_Comicsys.Models;

public class ComicBooks
{
    [Key]
    public int ComicBookId { get; set; }
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(255)]
    public string Author { get; set; } = string.Empty;
    [Range(0, double.MaxValue, ErrorMessage = "Price per day must be a positive value.")]
    public decimal PricePerDay { get; set; } 
    
    public virtual ICollection<RentalDetails> RentalDetails { get; set; } =  new Collection<RentalDetails>();
}