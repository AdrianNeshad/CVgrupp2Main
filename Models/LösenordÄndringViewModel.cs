using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class LösenordÄndringViewModel
    {
        [Required(ErrorMessage = "Skriv in ditt nya lösenord.")]
        [DataType(DataType.Password)]
        public string gammaltLösenord { get; set; }

        [Required(ErrorMessage = "Skriv in ditt nya lösenord.")]
        [DataType(DataType.Password)]
        [Compare("bekräftaNyttLösenord")]
        public string nyttLösenord { get; set; }

        [Required(ErrorMessage = "Bekräfta ditt nya lösenord")]
        [DataType(DataType.Password)]
        [Compare("bekräftaNyttLösenord")]
        public string bekräftaNyttLösenord { get; set; }
    }
}
