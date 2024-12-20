using System.ComponentModel.DataAnnotations.Schema;

namespace CVgrupp2Main.DatabasLager
{
    public class PersonSkickatMeddelande
    {
        public string Användarnamn { get; set; }
        public int MeddelandeID { get; set; }

        [ForeignKey(nameof(Användarnamn))]
        public virtual Person Person { get; set; }

        [ForeignKey(nameof(MeddelandeID))]
        public virtual Meddelande Meddelande { get; set; }
    }
}
