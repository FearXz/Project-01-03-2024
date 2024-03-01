using System.ComponentModel.DataAnnotations;

namespace Project_01_03_2024.Models
{
    public class Verbale : DbVerbale
    {

        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string NomeCompleto => $"{Nome} {Cognome}";
        [Display(Name = "Tipo Di Violazione")]
        public string Descrizione { get; set; }
        public int TotaleVerbali { get; set; }
    }
}