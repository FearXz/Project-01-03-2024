namespace Project_01_03_2024.Models
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string NomeCompleto => $"{Nome} {Cognome}";
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string CodiceFiscale { get; set; }
        public int TotaleViolazioni { get; set; }
        public int TotalePuntiRimossi { get; set; }
    }
}