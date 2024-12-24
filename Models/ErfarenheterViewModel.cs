using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class ErfarenheterViewModel
    {

        [Required(ErrorMessage = "PersonID måste anges")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Erfarenhetens titel måste anges")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Erfarenhetsbeskrivning måste anges")]
        public string Beskrivning { get; set; }
    }
}
