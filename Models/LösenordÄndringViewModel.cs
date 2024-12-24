using System.ComponentModel.DataAnnotations;

namespace CVgrupp2Main.Models
{
    public class LösenordÄndringViewModel
    {
        [Required(ErrorMessage = "Skriv in ditt gamla lösenord")]
        [DataType(DataType.Password)]
        public required string GammaltLösenord { get; set; }

        [Required(ErrorMessage = "Skriv in ditt nya lösenord")]
        [DataType(DataType.Password)]
        [Compare("bekräftaNyttLösenord")]
        public required string NyttLösenord { get; set; }

        [Required(ErrorMessage = "Bekräfta ditt nya lösenord")]
        [DataType(DataType.Password)]
        [Compare("bekräftaNyttLösenord")]
        public required string BekräftaNyttLösenord { get; set; }
    }
}
