using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoneyMarket.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ShortDiscription { get; set; }
        public string? Description { get; set; }
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
        
        [Display(Name = "Type Name")]
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual ApplicationType? ApplicationType { get; set; }
    }
}
