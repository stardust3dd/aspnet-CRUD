using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        [Key] // key annotation, this tells ef core that following line is primary key
        public int ID { get; set; }
        [Required]
        [DisplayName("Category name")]
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Range(1, 100, ErrorMessage="Display order value must be between 1 and 100!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
