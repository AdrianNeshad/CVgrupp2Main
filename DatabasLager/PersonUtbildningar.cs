using System.ComponentModel.DataAnnotations.Schema;

namespace CVgrupp2Main.DatabasLager
{
    public class PersonUtbildningar
    {
        public int UtbildningID { get; set; }
        public string PersonID { get; set; }

        [ForeignKey(nameof(UtbildningID))]
        public virtual Utbildningar Utbildningar { get; set; }

        [ForeignKey(nameof(PersonID))]
        public virtual Person Person { get; set; }
    }
}
