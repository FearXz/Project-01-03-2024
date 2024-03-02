using System.ComponentModel.DataAnnotations;

namespace Project_01_03_2024.Models
{
    public class DbAnagrafica
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }

        [StringLength(16, MinimumLength = 16)]
        public string CodiceFiscale { get; set; }
    }
}