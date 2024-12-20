using System.ComponentModel.DataAnnotations.Schema;

namespace CVgrupp2Main.DatabasLager
{
    public class PersonKompetenser
    {
        public int ID { get; set; }
        public string PersonID { get; set; }

        [ForeignKey(nameof(ID))]
        public virtual Kompetenser Kompetenser { get; set; }

        [ForeignKey(nameof(PersonID))]
        public virtual Person Person { get; set; }
    }
}
