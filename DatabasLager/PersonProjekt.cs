using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.DatabasLager
{
    public class PersonProjekt
    {
        [Column(Order = 0)]
        [MaxLength(255)]
        public int ProjektID { get; set; }

        [Column(Order = 1)]
        [MaxLength(255)]
        public string Medverkande { get; set; }

        [ForeignKey(nameof(ProjektID))]

        public virtual Projekt Projekt { get; set; }

        [ForeignKey(nameof(Medverkande))]
        public virtual Person Person { get; set; }
    }
}
