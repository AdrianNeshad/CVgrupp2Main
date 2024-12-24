using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class PersonViewModel
    {
        [Required (ErrorMessage = "Skriv användarnamn")]
        public required string Användarnamn { get; set; }

        [Required(ErrorMessage ="Skriv in ett lösenord")]
        [DataType (DataType.Password)]
        [Compare("BekräftaLösenord")]
        public required string Lösenord { get; set; }

        [Required(ErrorMessage = "Bekräfta lösenordet")]
        [DataType (DataType.Password)]
        [Compare("Lösenord")]
        [Display(Name = "Bekräfta Lösenordet")]
        public required string BekräftaLösenord { get; set; }
    }
}
