namespace CVgrupp2Main.DatabasLager
{
    public class Erfarenheter
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public virtual IEnumerable<PersonErfarenheter> HarErfarenhet { get; set; } = new List<PersonErfarenheter>();
    }
}
