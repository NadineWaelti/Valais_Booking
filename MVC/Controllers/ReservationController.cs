using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ReservationController : Controller
    {
        string baseUri = "http://localhost:12125/api/";


        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }


        //Ajoute une réservation à la suite de la recherche Search et affiche la confirmation
        public ActionResult ReservationSimple(string nom, string prenom, string entre, string sort, int numero, string nomlieu, decimal [] prices, int[] nums)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(sort);

            //Liste des chambres libres
            List<Room> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "RoomSearch/" + nomlieu + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Room>>(response.Result);

            }

            MVC.ViewModels.SearchVM test = new ViewModels.SearchVM
            {
                Chambres = ListProd

            };


            //Est-ce que le numéro fait partie de la liste des chambres libres ?
            bool trouve = false;

            for (int i = 0; i < test.Chambres.Count(); i++)
            {
                if (numero == test.Chambres.ElementAt(i).Id)
                {
                    trouve = true;
                }

            }

            //s'il n'y a pas de chambre
            if (nums == null)
            {
                trouve = false;
            }

            
            if (trouve == false)
            {
                return RedirectToAction("Index", "Home");
            }


            //Si la chambre n'a rien à voir
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == numero)
                {
                    trouve = true;
                }
                trouve = false;
            }

            //Si non, redirection sur la page d'accueil
            if (trouve == false)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Récupérer le prix
                decimal prix = 0;
                for(int i = 0; i < nums.Length; i++)
                {
                    if(nums[i] == numero)
                    {
                        prix = prices[i];
                    }
                }
              
                

                //Créer la réservation
                Reservation r = new Reservation() { Debut = entree, Fin = sortie, NomClient = nom, PrenomClient = prenom , Price = prix};
                PostReservation(r);


                List<Reservation> ListProdReservation = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    string uri = baseUri + "Reservations/" + entre + "/" + sort + "/" + nom + "/" + prenom;
                    Task<String> response = httpClient.GetStringAsync(uri);
                    ListProdReservation = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);

                }
                //récupérer l'id de la réservation
                Reservation IdReservation = ListProdReservation.Last();

                //Récupérer la chambre concernée
                Room ListProdRoom = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    string uri = baseUri + "Rooms/" + numero;
                    Task<String> response = httpClient.GetStringAsync(uri);
                    ListProdRoom = JsonConvert.DeserializeObject<Room>(response.Result);

                }

                //Créer le lien
                Lien l = new Lien() { ReservationId = IdReservation.Id, RoomId = ListProdRoom.Id };
                PostLien(l);

                ViewBag.code = IdReservation.Id;
                ViewBag.nom = nom;
                ViewBag.prenom = prenom;
                return View();
            }


        }



        //Insert Reservation
        public bool PostReservation(Reservation reservation)
        {
            string uri = baseUri + "/Reservations";
            using (HttpClient httpClient = new HttpClient())
            {
                string pro = JsonConvert.SerializeObject(reservation);
                StringContent frame = new StringContent(pro, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = httpClient.PostAsync(uri, frame);
                return response.Result.IsSuccessStatusCode;
            }
        }

        //Insert Lien
        public bool PostLien(Lien lien)
        {
            string uri = baseUri + "/Liens";
            using (HttpClient httpClient = new HttpClient())
            {
                string pro = JsonConvert.SerializeObject(lien);
                StringContent frame = new StringContent(pro, Encoding.UTF8, "Application/json");
                Task<HttpResponseMessage> response = httpClient.PostAsync(uri, frame);
                return response.Result.IsSuccessStatusCode;
            }
        }



        //Ajoute une réservation pour un groupe et affiche la confirmation
        public ActionResult ReservationGroupe(string nom, string prenom, string entre, string sort, int[] numero, decimal prix)
        {
            //S'il n'y a pas de chambre proposée mais que l'utilisateur essaie de réserver, il est redirigé sur la page d'accueil
            if (numero is null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                DateTime entree = DateTime.Parse(entre);
                DateTime sortie = DateTime.Parse(sort);


                //Créer la réservation
                Reservation r = new Reservation() { Debut = entree, Fin = sortie, NomClient = nom, PrenomClient = prenom, Price = prix };
                PostReservation(r);
                List<Reservation> ListProdReservation = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    string uri = baseUri + "Reservations/" + entre + "/" + sort + "/" + nom + "/" + prenom;
                    Task<String> response = httpClient.GetStringAsync(uri);
                    ListProdReservation = JsonConvert.DeserializeObject<List<Reservation>>(response.Result);

                }
                //récupérer l'id de la réservation
                Reservation IdReservation = ListProdReservation.Last();

                foreach (int i in numero)
                {
                    //Récupérer la chambre concernée
                    Room ListProdRoom = null;
                    using (HttpClient httpClient = new HttpClient())
                    {
                        string uri = baseUri + "Rooms/" + i;
                        Task<String> response = httpClient.GetStringAsync(uri);
                        ListProdRoom = JsonConvert.DeserializeObject<Room>(response.Result);

                    }
                    //Créer le lien
                    Lien l = new Lien() { ReservationId = IdReservation.Id, RoomId = ListProdRoom.Id };
                    PostLien(l);
                }


                ViewBag.code = IdReservation.Id;
                ViewBag.nom = nom;
                ViewBag.prenom = prenom;
                return View();
            }


        }

    }
}