using System.ComponentModel.DataAnnotations;

namespace HoneyOnlineStore.Models
{
    public class ApplicationType
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
