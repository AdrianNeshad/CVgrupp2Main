using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class KontaktuppgifterViewModel
    {
        public int KontaktID { get; set; }

        [Required(ErrorMessage = "Telefonnummer måste anges")]
        [Phone(ErrorMessage = "Ogiltigt telefonnummer")]
        public string Telefonnummer { get; set; }

        [Required(ErrorMessage = "Adress måste anges")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "E-postadress måste anges")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        public string Förnamn { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        public string Efternamn { get; set; }

    }
}