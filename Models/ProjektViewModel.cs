using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace CVgrupp2Main.Models
{
    public class ProjektViewModel
    {
        public int ProjektID { get; set; }

        [Required(ErrorMessage = "Vänligen skriv ett projektnamn.")]
        public required string Namn {  get; set; }

        [Required(ErrorMessage = "Vänligen fyll i en projektbeskrivning.")]
        public required string Beskrivning { get; set; }

    }
}
