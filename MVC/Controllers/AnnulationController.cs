using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AnnulationController : Controller
    {
        string baseUri = "http://localhost:12125/api/";


        // GET: Annulation
        public ActionResult Index()
        {
            return View();
        }

        //Annuler une réservation
        public ActionResult Annuler(string nom, string prenom, int code)
        {

            List<Reservation> res = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "Reservations/" + nom + "/" + prenom + "/" + code;
                Task<String> response = httpClient.GetStringAsync(uri);
                res = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);

            }


            List<Lien> trouve = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "LiensDelete/" + code;
                Task<String> response = httpClient.GetStringAsync(uri);
                trouve = JsonConvert.DeserializeObject<List<Lien>>(response.Result);

            }
            //Si rien trouvé 
            if (trouve.FirstOrDefault() == null)
            {
                return RedirectToAction("PasTrouve", "Annulation");
            }

            if (res.FirstOrDefault() == null)
            {
                return RedirectToAction("TesLeFilsAqui", "Annulation");
            }

            //Delete Lien
            DeleteLien(trouve.FirstOrDefault().Id);
            //Delete Reservation
            DeleteReservation(res.FirstOrDefault().Id);
            return View();
        }

        public bool DeleteLien(int i)
        {
            string uri = baseUri + "Liens" + "/" + i;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> response = httpClient.DeleteAsync(uri);
                return response.Result.IsSuccessStatusCode;
            }
        }

        public bool DeleteReservation(int i)
        {
            string uri = baseUri + "Reservations" + "/" + i;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<HttpResponseMessage> response = httpClient.DeleteAsync(uri);
                return response.Result.IsSuccessStatusCode;
            }
        }


        //Vue quand le code n'est pas trouvé
        public ActionResult PasTrouve()
        {
            return View();
        }

        //Vue quand le nom ou le prénom ne sont pas trouvés
        public ActionResult TesLeFilsAqui()
        {
            return View();
        }
    }
}