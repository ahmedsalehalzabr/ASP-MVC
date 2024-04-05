using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models
{
    public class Item
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
