using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MoonSpace.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Kategori måste fyllas i!")]
        [DisplayName("Kategori")]
        public string Type { get; set; }
    }
}
