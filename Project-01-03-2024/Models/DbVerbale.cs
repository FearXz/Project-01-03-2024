using System;
using System.ComponentModel.DataAnnotations;

namespace Project_01_03_2024.Models
{
    public class DbVerbale
    {
        public int IdVerbale { get; set; }
        [Display(Name = "Criminale")]
        public int IdUtente { get; set; }
        public int IdViolazione { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string NomeAgente { get; set; }
        public DateTime DataTrascrizione { get; set; }
        public decimal Importo { get; set; }
        public int PuntiDecurtati { get; set; }
    }
}