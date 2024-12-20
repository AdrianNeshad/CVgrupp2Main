using CVgrupp2Main.DatabasLager;

namespace CVgrupp2Main.Models
{
    public class PersonProjektViewModel
    {
        public List<Person> Person { get; set; }
        public List<Projekt> Projekt { get; set; }
        public List<PersonProjekt> PersonProjekt { get; set; }
    }
}
