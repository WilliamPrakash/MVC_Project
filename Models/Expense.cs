using System.ComponentModel.DataAnnotations; // [Required] attribute

namespace MVC_Project.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        [Required]
        public string? Description{ get; set; } // don't understand why it's nullable but also required?
    }
}
