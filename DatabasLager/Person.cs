using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.DatabasLager
{
    public class Person
    {
        [Key]
        [MaxLength(255)]
        public string Användarnamn { get; set; }
        public string? Förnamn { get; set; }
        public string? Efternamn { get; set; }
        public string? Lösenord { get; set; }
        public bool Privat { get; set; }

        public byte[]? ProfilBild { get; set; }
        public int kontaktID { get; set; }

        [ForeignKey(nameof(kontaktID))]
        public virtual Kontaktuppgifter Kontaktuppgifter { get; set; }
        public virtual IEnumerable<PersonErfarenheter> HarErfarenhet { get; set; } = new List<PersonErfarenheter>();
        public virtual IEnumerable<PersonKompetenser> HarKompetens { get; set; } = new List<PersonKompetenser>();
        public virtual IEnumerable<PersonMottagitMeddelande> HarMottagit { get; set; } = new List<PersonMottagitMeddelande>();
        public virtual IEnumerable<PersonSkickatMeddelande> HarSkickat { get; set; } = new List<PersonSkickatMeddelande>();
        public virtual IEnumerable<PersonProjekt> HarMedverkat { get; set; } = new List<PersonProjekt>();
        public virtual IEnumerable<PersonUtbildningar> HarUtbildning { get; set; } = new List<PersonUtbildningar>();
        public virtual IEnumerable<Projekt> HarProjekt { get; set; } = new List<Projekt>();
    }
}
