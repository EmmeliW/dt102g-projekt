using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoonSpace.Models
{
    public class Costumer
    {
        [Key]
        [Required]
        [DisplayName("Kund ID")]
        public int CostumerId { get; set; }

        [Required(ErrorMessage = "Förnamn är obigatoriskt!")]
        [DisplayName("Förnamn")]
        [MinLength(2, ErrorMessage = "Förnamn måste innehålla minst två tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn är obligatoriskt!")]
        [DisplayName("Efternamn")]
        [MinLength(2, ErrorMessage = "Efternamn måste innehålla minst två tecken.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-postardess är obilgatoriskt!")]
        [EmailAddress (ErrorMessage = "Du måste ange en giltig e-postadress")]
        [DisplayName("E-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefonnummer är obilgatorisk!")]
        [Phone(ErrorMessage = "Du måste ange ett giltigt telefonnummer")]
        [DisplayName("Telefonnummer")]
        public string PhoneNumber { get; set; }
    }
}
