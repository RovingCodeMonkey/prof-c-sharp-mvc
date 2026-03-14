using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DiscountId { get; set; }
        
        public long ProductId { get; set; }

        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountPercentage { get; set; }

        [ForeignKey("ProductId")]
        public virtual required Product Product { get; set; }
    }
}
