using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HoneyOnlineStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The Order Number is not allow.")]
        public int OrderCategory { get; set; }
    }
}
