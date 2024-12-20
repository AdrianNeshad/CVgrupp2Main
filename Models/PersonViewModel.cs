using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class PersonViewModel
    {
        [Required (ErrorMessage = "Skriv användarnamn")]
        public string Användarnamn { get; set; }

        [Required(ErrorMessage ="Skriv in ett lösenord")]
        [DataType (DataType.Password)]
        [Compare("BekräftaLösenord")]
        public string Lösenord { get; set; }

        [Required(ErrorMessage = "Bekräfta lösenordet")]
        [DataType (DataType.Password)]
        [Compare("Lösenord")]
        [Display(Name = "Bekräfta Lösenordet")]
        public string BekräftaLösenord { get; set; }
    }
}
