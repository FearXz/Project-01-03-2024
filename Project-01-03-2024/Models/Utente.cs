namespace Project_01_03_2024.Models
{
    public class Utente : DbAnagrafica
    {
        public string NomeCompleto => $"{Nome} {Cognome}";
        public int TotaleViolazioni { get; set; }
        public int TotalePuntiRimossi { get; set; }
    }
}