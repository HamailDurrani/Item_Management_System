using System.ComponentModel.DataAnnotations;

namespace ItemManagementSystem.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
