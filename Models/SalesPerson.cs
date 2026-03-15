using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Web.Models
{
    [Index(nameof(FirstName), nameof(LastName), nameof(Phone), IsUnique = true)]
    public class SalesPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesPersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; } 
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public string Manager { get; set; }

    }
}
