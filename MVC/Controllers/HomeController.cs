using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        string baseUri = "http://localhost:12125/api/";

        //Page d'accueil
        public ActionResult Index()
        {
            return View();
        }

        //Présentation des hôtels
        public ActionResult Hotel()
        {
            string uri = baseUri + "Hotels/";

            List<Hotel> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
                MVC.ViewModels.ToutesMesListes formulaire = new ViewModels.ToutesMesListes
                {
                    LesHotels = ListProd,
                };
                return View(formulaire);
            }
        }

        //Présentation de toutes les chambres selon l'hôtel
        public ActionResult Chambre(string nomHotel)
        {
            string uri = baseUri + "Hotel/" + nomHotel;
            string uriRoom = baseUri + "Room/" + nomHotel;
            string uriPicture = baseUri + "Picture/" + nomHotel;

            List<Hotel> ListProd = null;
            List<Room> ListProdRoom = null;
            List<Picture> ListProdPicture = null;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> responseRoom = httpClient.GetStringAsync(uriRoom);
                ListProdRoom = JsonConvert.DeserializeObject<List<Room>>(responseRoom.Result);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> responsePicture = httpClient.GetStringAsync(uriPicture);
                ListProdPicture = JsonConvert.DeserializeObject<List<Picture>>(responsePicture.Result);
            }
            MVC.ViewModels.SearchVM formulaire = new ViewModels.SearchVM
            {
                Chambres = ListProdRoom,
                Picture = ListProdPicture,
                LesHotels = ListProd,
            };

            return View(formulaire);
        }

        //Formulaire pour la recherche simple
        public ActionResult Formulaire()
        {
            string uri = baseUri + "Hotels/";
            List<Hotel> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
                return View(ListProd);
            }
        }

        //Formulaire pour la recherche avancée
        public ActionResult Edit()
        {
            string uri = baseUri + "Hotels/";
            string uriRoom = baseUri + "Rooms/";

            List<Hotel> ListProd = null;
            List<Room> ListProdRoom = null;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
                Task<String> responseRoom = httpClient.GetStringAsync(uriRoom);
                ListProdRoom = JsonConvert.DeserializeObject<List<Room>>(response.Result);
                MVC.ViewModels.ToutesMesListes formulaire = new ViewModels.ToutesMesListes
                {
                    LesHotels = ListProd,
                    Chambres = ListProdRoom,
                };
                return View(formulaire);
            }
        }

        //Formulaire pour la réservation de groupe
        public ActionResult FormulaireGroupe()
        {
            string uri = baseUri + "Hotels/";
            List<Hotel> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
                return View(ListProd);
            }
        }

        //Annuler une réservation
        public ActionResult About()
        {
            return View();
        }

        //Formulaire mail
        public ActionResult Contact()
        {
            return View();
        }

        //Envoi du mail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(MailModel model)
        {
            //Code trouvé sur : https://www.mikesdotnetting.com/article/268/how-to-send-email-in-asp-net-mvc
            //adapté pour que les message arrivent sur ma boîte nadine.waelti1990@gmail.com

            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("nadine.waelti1990@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("sender@outlook.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "nadine.waelti1990@gmail.com",  // replace with valid value
                        Password = "waelnadi90"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        //Confirmation de l'envoi du mail
        public ActionResult Sent()
        {
            return View();
        }

        //Espace client
        public ActionResult login ()
        {
            return View();
        }


        //Les réservations en cours pour un client x
        public ActionResult Overview(string nom, string prenom)
        {
            //rechercher les réservations à ce nom et prénom
            List<Reservation> res = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "ReservationsOverview/" + nom + "/" + prenom;
                Task<String> response = httpClient.GetStringAsync(uri);
                res = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);

            }

            // Pour afficher les numéros des chambres             
            List<Lien> liens = null;
            foreach(Reservation reservation in res)
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string uri = baseUri + "Liens/";
                    Task<String> response = httpClient.GetStringAsync(uri);
                    liens = JsonConvert.DeserializeObject<List<Lien>>(response.Result);                    
                }
            }
            MVC.ViewModels.OverviewVM formulaire = new ViewModels.OverviewVM
            {
                Reservations = res,
                Liens = liens,
            };

            return View(formulaire);
        }
    }
}