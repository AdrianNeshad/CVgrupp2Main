﻿using System.ComponentModel.DataAnnotations;
namespace CVgrupp2Main.DatabasLager
{
    public class Utbildningar
    {
        [Key]
        public int UtbildningID { get; set; }
        public string? Namn { get; set; }
        public string? Beskrivning { get; set; }
        public virtual IEnumerable<PersonUtbildningar> HarUtbildning { get; set; } = new List<PersonUtbildningar>();
    }
}
