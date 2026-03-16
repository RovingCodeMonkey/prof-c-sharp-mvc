using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    [Index(nameof(FirstName), nameof(LastName), nameof(Phone), IsUnique = true)]
    public class SalesPerson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SalesPersonId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(500)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        [MaxLength(200)]
        public string? Manager { get; set; }
    }
}
