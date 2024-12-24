using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class MeddelandeViewModel
    {
        [Required(ErrorMessage = "Ange giltigt användarnamn")]
        public string Avsändare { get; set; }

        [Required(ErrorMessage = "Ange giltigt användarnamn")]
        public string Mottagare { get; set; }

        [Required(ErrorMessage = "Du måste skriva någonting som meddelande")]
        public string Meddelande { get; set; }
    }
}
