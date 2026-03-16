using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesId { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public long SalesPersonId { get; set; }
        [Required]
        public long CustomerId { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        public decimal AppliedDiscount { get; set; }
        [Required]
        public decimal FinalPrice { get; set; }
        public decimal CommisionPercentage { get; set; }
        public decimal Commision { get; set; }
        [Required]
        public DateTime SalesDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }        

        [ForeignKey("SalesPersonId")]
        public virtual SalesPerson? SalesPerson { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
