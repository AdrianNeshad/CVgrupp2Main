using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.DatabasLager
{
    public class Meddelande
    {
        public int ID { get; set; }

        [StringLength(255, ErrorMessage = "Meddelandet får ha max 255 tecken.")]
        public string Innehåll { get; set; }

        public bool HarLästs { get; set; }
        public virtual IEnumerable<PersonMottagitMeddelande> HarMottagit { get; set; } = new List<PersonMottagitMeddelande>();
        public virtual IEnumerable<PersonSkickatMeddelande> HarSkickat { get; set; } = new List<PersonSkickatMeddelande>();
    }
}
