namespace CVgrupp2Main.DatabasLager
{
    public class Kontaktuppgifter
    {
        public int KontaktID { get; set; }
        public string Telefonnummer { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public virtual Person Person { get; set; }
    }
}
