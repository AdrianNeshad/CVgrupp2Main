using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class KontaktuppgifterViewModel
    {
        public int KontaktID { get; set; }

        [Required(ErrorMessage = "Telefonnummer måste anges")]
        [Phone(ErrorMessage = "Ogiltigt telefonnummer")]
        public required string Telefonnummer { get; set; }

        [Required(ErrorMessage = "Adress måste anges")]
        public required string Adress { get; set; }

        [Required(ErrorMessage = "E-postadress måste anges")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        public required string Förnamn { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        public required string Efternamn { get; set; }

    }
}