using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Style { get; set; }
        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public double SalePrice { get; set; }
        public int QtyOnHand { get; set; }
        public double CommisionPercentage { get; set; }


    }
}
