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
            List<Verbale> listaVerbali = GetListaVerbali();
            return View(listaVerbali);
        }
        // NuovoVerbale usa i metodi GetListaTipoViolazioni e GetListaUtenti per popolare le dropdownlist
        public ActionResult NuovoVerbale()
        {
            ViewBag.ListaTipoViolazioni = GetListaTipoViolazioni();
            ViewBag.ListaUtenti = GetListaUtenti();

            return View();
        }
        //NuovoVerbale  [httpPost] inserisce un nuovo verbale nel database
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

                ViewBag.ListaTipoViolazioni = GetListaTipoViolazioni();
                ViewBag.ListaUtenti = GetListaUtenti();
                TempData["message"] = "Verbale inserito correttamente";
                return RedirectToAction("Index", "Verbali");
            }
            else
            {
                return View();
            }
        }
        // GetListaTipoViolazioni restituisce una lista di oggetti TipoViolazione
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
        // GetListaUtenti restituisce una lista di oggetti Utente
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
        // GetListaVerbali restituisce una lista di oggetti Verbale
        private List<Verbale> GetListaVerbali()
        {
            List<Verbale> listaVerbali = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT v.IdVerbale,a.IdAnagrafica,a.Nome,a.Cognome,v.IdViolazione,t.Descrizione,v.DataViolazione,v.IndirizzoViolazione,v.Nominativo_Agente,v.DataTrascrizioneVerbale,v.Importo,v.DecurtamentoPunti FROM VERBALE AS V INNER JOIN ANAGRAFICA AS A ON A.IdAnagrafica=V.IdAnagrafica INNER JOIN TIPO_VIOLAZIONE AS T ON T.IdViolazione = V.IdViolazione";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Verbale newVerbale = new Verbale
                            {
                                IdVerbale = reader.GetInt32(0),
                                IdUtente = reader.GetInt32(1),
                                Nome = reader.GetString(2),
                                Cognome = reader.GetString(3),
                                IdViolazione = reader.GetInt32(4),
                                Descrizione = reader.GetString(5),
                                DataViolazione = reader.GetDateTime(6),
                                IndirizzoViolazione = reader.GetString(7),
                                NomeAgente = reader.GetString(8),
                                DataTrascrizione = reader.GetDateTime(9),
                                Importo = reader.GetDecimal(10),
                                PuntiDecurtati = reader.GetInt32(11)
                            };
                            listaVerbali.Add(newVerbale);
                        }
                    }
                }
                conn.Close();
            }
            return listaVerbali;

        }

    }
}