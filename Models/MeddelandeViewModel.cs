using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class MeddelandeViewModel
    {
        [Required(ErrorMessage = "Ange giltigt användarnamn")]
        public required string Avsändare { get; set; }

        [Required(ErrorMessage = "Ange giltigt användarnamn")]
        public required string Mottagare { get; set; }

        [Required(ErrorMessage = "Du måste skriva någonting som meddelande")]
        public required string Meddelande { get; set; }
    }
}
