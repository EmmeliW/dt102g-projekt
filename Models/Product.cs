using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoonSpace.Models
{
    public class Product
    {
        [Key]
        [Required]
        [DisplayName("Produkt Id")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [DisplayName("Produktnamn")]
        public string ProductName { get; set; }

        [Required]
        [Range(1, 50000, ErrorMessage = "Priset måste ligga mellan 1-50000 kr")]
        [DisplayName("Pris")]
        public int ProductPrice { get; set; }

        [Required]
        [ForeignKey("Category")]
        [DisplayName("Produkttyp")]
        public string Category { get; set; }

        [Required]
        [DisplayName("Beskrivning")]
        [MinLength(100, ErrorMessage = "Beskrivning måste innehålla minst 100 tecken.")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Bildnamn")]
        public string ImageName { get; set; }
    }
}
