using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class UtbildningarViewModel
    {
        public string PersonID { get; set; }

        [Required(ErrorMessage = "Utbildningens namn måste anges")]
        public string Namn { get; set; }

        [Required(ErrorMessage = "Utbildningens beskrivning måste anges")]
        public string Beskrivning { get; set; }
    }
}
