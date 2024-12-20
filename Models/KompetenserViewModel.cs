using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class KompetenserViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Fyll i kompetenstitel")]
        [StringLength(50, ErrorMessage = "Kompetenstitel får vara max 50 karaktärer")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Fyll i kompetensbeskrivning")]
        [StringLength(300, ErrorMessage = "Kompetensbeskrivning får vara max 300 karaktärer")]
        public string Beskrivning { get; set; }
    }
}
