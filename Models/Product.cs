using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Index(nameof(Name), nameof(Manufacturer), IsUnique = true)]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string Manufacturer { get; set; }
        [Required]
        [MaxLength(100)]
        public string Style { get; set; }
        [Required]
        public decimal PurchasePrice { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public decimal CommisionPercentage { get; set; }
    }
}
