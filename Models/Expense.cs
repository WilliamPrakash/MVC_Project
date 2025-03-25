using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Expense
    {
        [Column("Id")] // Helps to ensure correct column mapping
        public int Id { get; set; }
        [Column("Value")]
        public decimal Value { get; set; }
        [Required]
        [Column("Description")]
        public string? Description{ get; set; }
    }
}
