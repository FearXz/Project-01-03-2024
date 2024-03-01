using Project_01_03_2024.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Project_01_03_2024.Controllers
{
    public class RegistroController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _ListaViolazioni()
        {
            List<Violazione> listaViolazioni = GetViolazioni();

            return PartialView(listaViolazioni);
        }
        public ActionResult _TotaleVerbaliPerUtente()
        {
            List<Verbale> listaTotaleVerbaliPerUtente = GetTotaleVerbaliPerUtente();

            return PartialView(listaTotaleVerbaliPerUtente);
        }
        public ActionResult _TotalePuntiDecurtatiPerUtente()
        {
            List<Verbale> listaTotalePuntiDecurtatiPerUtente = GetTotalePuntiDecurtatiPerUtente();

            return PartialView(listaTotalePuntiDecurtatiPerUtente);
        }
        public ActionResult _VerbaliMaggiore400()
        {
            List<Verbale> listaVerbaliMaggiore400 = GetVerbaliMaggiore400();

            return PartialView(listaVerbaliMaggiore400);
        }
        public ActionResult _VerbaliMagiore10punti()
        {
            List<Verbale> listaVerbaliMaggiore10punti = GetVerbaliMaggiore10punti();

            return PartialView(listaVerbaliMaggiore10punti);
        }
        private List<Violazione> GetViolazioni()
        {
            List<Violazione> listaViolazioni = new List<Violazione>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT * FROM TIPO_VIOLAZIONE";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Violazione violazione = new Violazione
                        {
                            IdViolazione = reader.GetInt32(0),
                            Descrizione = reader.GetString(1),
                        };
                        listaViolazioni.Add(violazione);
                    }
                }
            }
            return listaViolazioni;
        }
        private List<Verbale> GetTotaleVerbaliPerUtente()
        {
            List<Verbale> listaTotaleVerbaliPerUtente = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT a.Nome,a.Cognome,COUNT(v.IdVerbale) as TotaleVerbali FROM VERBALE AS V INNER JOIN ANAGRAFICA AS A ON A.IdAnagrafica= V.IdAnagrafica INNER JOIN TIPO_VIOLAZIONE AS T ON T.IdViolazione=V.IdViolazione GROUP BY a.Nome, a.Cognome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale
                        {
                            Nome = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            TotaleVerbali = reader.GetInt32(2),
                        };
                        listaTotaleVerbaliPerUtente.Add(verbale);
                    }
                }
            }
            return listaTotaleVerbaliPerUtente;
        }
        private List<Verbale> GetTotalePuntiDecurtatiPerUtente()
        {
            List<Verbale> listaTotalePuntiDecurtatiPerUtente = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT Cognome, Nome, SUM(DecurtamentoPunti) AS TotalePunti FROM Anagrafica INNER JOIN Verbale ON Anagrafica.IdAnagrafica = Verbale.IDAnagrafica GROUP BY Cognome, Nome";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale
                        {
                            Nome = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            PuntiDecurtati = reader.GetInt32(2),
                        };
                        listaTotalePuntiDecurtatiPerUtente.Add(verbale);
                    }
                }
            }
            return listaTotalePuntiDecurtatiPerUtente;
        }
        private List<Verbale> GetVerbaliMaggiore400()
        {
            List<Verbale> listaVerbaliMaggiore400 = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT a.NOME , a.cognome , ve.importo , vi.descrizione FROM anagrafica as a inner join verbale as ve on ve.idanagrafica = a.IdAnagrafica inner join tipo_violazione as vi on ve.IDViolazione = vi.IDViolazione where ve.importo > 400";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale
                        {
                            Nome = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            Importo = reader.GetDecimal(2),
                            Descrizione = reader.GetString(3),
                        };
                        listaVerbaliMaggiore400.Add(verbale);
                    }
                }
            }
            return listaVerbaliMaggiore400;
        }
        private List<Verbale> GetVerbaliMaggiore10punti()
        {
            List<Verbale> listaVerbaliMaggiore10punti = new List<Verbale>();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT a.nome , a.cognome , v.importo , v.DataViolazione , v.DecurtamentoPunti FROM Anagrafica as a inner join verbale as v on a.IdAnagrafica = v.IDAnagrafica WHERE DecurtamentoPunti >= 10";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Verbale verbale = new Verbale
                        {
                            Nome = reader.GetString(0),
                            Cognome = reader.GetString(1),
                            Importo = reader.GetDecimal(2),
                            DataViolazione = reader.GetDateTime(3),
                            PuntiDecurtati = reader.GetInt32(4),
                        };
                        listaVerbaliMaggiore10punti.Add(verbale);
                    }
                }
            }
            return listaVerbaliMaggiore10punti;
        }
    }
}