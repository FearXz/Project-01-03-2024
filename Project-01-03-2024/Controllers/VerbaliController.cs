using Project_01_03_2024.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Project_01_03_2024.Controllers
{
    public class VerbaliController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("NuovoVerbale");
        }

        public ActionResult NuovoVerbale()
        {
            ViewBag.ListaTipoViolazioni = GetListaTipoViolazioni();
            ViewBag.ListaUtenti = GetListaUtenti();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuovoVerbale(Verbale verbale)
        {
            if (ModelState.IsValid)
            {

                using (SqlConnection conn = Connection.GetConn())
                {
                    conn.Open();

                    string query = "INSERT INTO VERBALE (IdAnagrafica,IdViolazione,DataViolazione,IndirizzoViolazione,Nominativo_Agente,DataTrascrizioneVerbale,Importo,DecurtamentoPunti) " +
                        "VALUES (@IdUtente,@IdViolazione,@DataViolazione,@IndirizzoViolazione,@NomeAgente,@DataTrascrizione,@Importo,@PuntiDecurtati)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUtente", verbale.IdUtente);
                        cmd.Parameters.AddWithValue("@IdViolazione", verbale.IdViolazione);
                        cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                        cmd.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                        cmd.Parameters.AddWithValue("@NomeAgente", verbale.NomeAgente);
                        cmd.Parameters.AddWithValue("@DataTrascrizione", verbale.DataTrascrizione);
                        cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                        cmd.Parameters.AddWithValue("@PuntiDecurtati", verbale.PuntiDecurtati);

                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Verbale già registrato";
                return View();
            }
        }

        private List<Verbale> GetListaTipoViolazioni()
        {
            List<Verbale> listaTipoViolazioni = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT * FROM TIPO_VIOLAZIONE";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Verbale newVerbale = new Verbale
                            {
                                IdViolazione = reader.GetInt32(0),
                                Descrizione = reader.GetString(1)
                            };
                            listaTipoViolazioni.Add(newVerbale);
                        }
                    }
                }
                conn.Close();
            }
            return listaTipoViolazioni;
        }

        private List<Utente> GetListaUtenti()
        {
            List<Utente> listaUtenti = new List<Utente>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Utente newUtente = new Utente
                            {
                                IdUtente = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Cognome = reader.GetString(2),
                                Indirizzo = reader.GetString(3),
                                Citta = reader.GetString(4),
                                CAP = reader.GetString(5),
                                CodiceFiscale = reader.GetString(6)
                            };
                            listaUtenti.Add(newUtente);
                        }
                    }
                }
                conn.Close();
            }
            return listaUtenti;
        }
    }
}