using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoneyOnlineStore.Models
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public Guid JsonId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Products { get; set; }

    }
}
