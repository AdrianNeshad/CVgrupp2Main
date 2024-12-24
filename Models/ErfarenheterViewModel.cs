using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class ErfarenheterViewModel
    {

        [Required(ErrorMessage = "PersonID måste anges")]
        public required string ID { get; set; }

        [Required(ErrorMessage = "Erfarenhetens titel måste anges")]
        public required string Titel { get; set; }

        [Required(ErrorMessage = "Erfarenhetsbeskrivning måste anges")]
        public required string Beskrivning { get; set; }
    }
}
