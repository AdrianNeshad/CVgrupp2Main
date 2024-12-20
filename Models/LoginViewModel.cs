using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Du måste skriva in ett användarnamn")]
        public string Användarnamn { get; set; }

        [Required(ErrorMessage = "Du måste skriva in ett lösenord")]
        [DataType(DataType.Password)]
        public string Lösenord { get; set; }

        public bool Komihåg {  get; set; }


    }
}
