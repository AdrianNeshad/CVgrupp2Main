using System.ComponentModel.DataAnnotations.Schema;

namespace CVgrupp2Main.DatabasLager
{
    public class Projekt
    {
        public int ProjektID { get; set; }
        public string Namn { get; set; }
        public string Beskrivning { get; set; }
        public string Skapare { get; set; }
        [ForeignKey(nameof(Skapare))]
        public virtual Person Person { get; set; }
        public virtual IEnumerable<PersonProjekt> HarMedverkat { get; set; } = new List<PersonProjekt>();
    }
}
