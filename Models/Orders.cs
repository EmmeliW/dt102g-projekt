using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoonSpace.Models
{
    public class Orders
    {
        [Key]
        [Required]
        [DisplayName("Order ID")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        [DisplayName("Produkt")]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Order datum")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [DefaultValue(false)]
        [DisplayName("Betalad")]
        public bool Paid { get; set; }

        [Required]
        [ForeignKey("CostumerId")]
        [DisplayName("Kund ID")]
        public int CostumerId { get; set; }
    }
}
