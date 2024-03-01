﻿using Project_01_03_2024.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Project_01_03_2024.Controllers
{
    public class UtenteController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("RegistraUtente");
        }

        public ActionResult RegistraUtente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistraUtente(Utente utente)
        {
            if (ModelState.IsValid)
            {
                Utente utenteEsiste = CheckUtente(utente);

                if (utenteEsiste == null)
                {
                    using (SqlConnection conn = Connection.GetConn())
                    {
                        conn.Open();

                        string query = "INSERT INTO ANAGRAFICA (Cognome, Nome,  Indirizzo, Città, CAP, Cod_Fisc) VALUES (@Cognome,@Nome,  @Indirizzo, @Citta, @CAP, @CodiceFiscale)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nome", utente.Nome);
                            cmd.Parameters.AddWithValue("@Cognome", utente.Cognome);
                            cmd.Parameters.AddWithValue("@Indirizzo", utente.Indirizzo);
                            cmd.Parameters.AddWithValue("@Citta", utente.Citta);
                            cmd.Parameters.AddWithValue("@CAP", utente.CAP);
                            cmd.Parameters.AddWithValue("@CodiceFiscale", utente.CodiceFiscale);

                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Utente già registrato";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        private Utente CheckUtente(Utente utente)
        {
            Utente nuovoUtente = new Utente();

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();

                string query = "SELECT * FROM ANAGRAFICA WHERE Cod_Fisc = @CodiceFiscale";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CodiceFiscale", utente.CodiceFiscale);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            nuovoUtente.IdUtente = reader.GetInt32(0);
                            nuovoUtente.Cognome = reader.GetString(1);
                            nuovoUtente.Nome = reader.GetString(2);
                            nuovoUtente.Indirizzo = reader.GetString(3);
                            nuovoUtente.Citta = reader.GetString(4);
                            nuovoUtente.CAP = reader.GetString(5);
                            nuovoUtente.CodiceFiscale = reader.GetString(6);

                            return nuovoUtente;
                        }
                    }
                }
                conn.Close();
            }
            return null;
        }
    }
}