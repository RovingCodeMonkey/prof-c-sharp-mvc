using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesId { get; set; }

        public long ProductId { get; set; }        
        public long SalesPersonId { get; set; }
        public long CustomerId { get; set; }
        public DateTime SalesDate { get; set; }

        [ForeignKey("ProductId")]
        public virtual required Product Product { get; set; }        

        [ForeignKey("SalesPersonId")]
        public virtual required SalesPerson SalesPerson { get; set; }

        [ForeignKey("CustomerId")]
        public virtual required Customer Customer { get; set; }
    }
}
