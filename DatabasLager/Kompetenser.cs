namespace CVgrupp2Main.DatabasLager
{
    public class Kompetenser
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public virtual IEnumerable<PersonKompetenser> HarKompetens { get; set; } = new List<PersonKompetenser>();
    }
}
