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
    public class SearchController : Controller
    {
        string baseUri = "http://localhost:12125/api/";

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        //Résultat de la recherche Search
        public ActionResult Search(string nomlieu, string entre, string sort)
        {
            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(sort);

            //Si la date d'entrée est avant aujourd'hui
            if (entree < DateTime.Today)
            {
                return RedirectToAction("Formulaire", "Home");
            }

            //Si la date de sortie est inférieure à la date d'entrée
            if (sortie < entree)
            {
                return RedirectToAction("Formulaire", "Home");
            }

            //Si les dates sont identiques
            if (sortie == entree)
            {
                return RedirectToAction("Formulaire", "Home");
            }

            List<Hotel> ListProdHotels = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriHotels = baseUri + "Hotels/";
                Task<String> response = httpClient.GetStringAsync(uriHotels);
                ListProdHotels = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }


            //Liste des chambres demandée par l'utilisateur
            List<Room> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "RoomSearch/" + nomlieu + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Room>>(response.Result);

            }

            List<Picture> ListProdPictures = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriPictures = baseUri + "PictureSearch/" + nomlieu + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uriPictures);
                ListProdPictures = JsonConvert.DeserializeObject<List<Picture>>(response.Result);
            }

            //Add-on, changement du prix selon l'occupation
            //Nbre de chambres libres dans chaque hôtel :

            //Valais Palace
            List<Room> vs = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                vs = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double nbvalais = 0;
            foreach (Room i in vs)
            {
                nbvalais++;
            }

            //Grand Duc
            List<Room> gd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                gd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbduc = 0;
            foreach (Room i in gd)
            {
                nbduc++;
            }

            //Octodure
            List<Room> oc = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                oc = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nboctodure = 0;
            foreach (Room i in oc)
            {
                nboctodure++;
            }

            //Constantin Palace
            List<Room> co = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                co = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbconstantin = 0;
            foreach (Room i in co)
            {
                nbconstantin++;
            }

            //Walliser Palace
            List<Room> wa = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                wa = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbwalliser = 0;
            foreach (Room i in wa)
            {
                nbwalliser++;
            }

            //Matterhorn
            List<Room> ma = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                ma = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbmatterhorn = 0;
            foreach (Room i in ma)
            {
                nbmatterhorn++;
            }

            // Nbre total de chambres dans chaque hôtel:
            //Valais Palace
            List<Room> val = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                val = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double valais = 0;
            foreach (Room i in val)
            {
                valais++;
            }

            //Grand Duc
            List<Room> grd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                grd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double duc = 0;
            foreach (Room i in grd)
            {
                duc++;
            }

            //Octodure
            List<Room> oct = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                oct = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double octodure = 0;
            foreach (Room i in oct)
            {
                octodure++;
            }

            //Constantin Palace
            List<Room> con = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                con = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double constantin = 0;
            foreach (Room i in con)
            {
                constantin++;
            }

            //Walliser Palace
            List<Room> wal = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                wal = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double walliser = 0;
            foreach (Room i in wal)
            {
                walliser++;
            }

            //Matterhorn
            List<Room> mat = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                mat = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double matterhorn = 0;
            foreach (Room i in mat)
            {
                matterhorn++;
            }

            //Pourcentage de non-occupation
            double valPalace = nbvalais / valais;
            double grandDuc = nbduc / duc;
            double octo = nboctodure / octodure;
            double constPalace = nbconstantin / constantin;
            double mattHorn = nbmatterhorn / matterhorn;
            double wallPalace = nbwalliser / walliser;



            //Test pourcentage
            if (valPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 3)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (grandDuc <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 4)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (octo <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 1)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (constPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 2)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (mattHorn <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 6)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (wallPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 5)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }




            MVC.ViewModels.SearchVM formulaire = new ViewModels.SearchVM
            {
                Chambres = ListProd,
                LesHotels = ListProdHotels,
                Picture = ListProdPictures,
                Debut = entre,
                Fin = sort,
                Lieu = nomlieu
            };


            return View(formulaire);

        }


        //Resultat de la recherche avancée
        public ActionResult AdvancedSearch(string nomlieu, string entre, string sort, int categorie, bool wifi, bool parking, int type, decimal prix, bool tv, bool hairDryer)
        {
            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(sort);

            //Si la date d'entrée est avant aujourd'hui
            if (entree < DateTime.Today)
            {
                return RedirectToAction("Edit", "Home");
            }

            //Si la date de sortie est inférieure à la date d'entrée
            if (sortie < entree)
            {
                return RedirectToAction("Edit", "Home");
            }

            //Si les dates sont identiques
            if (sortie == entree)
            {
                return RedirectToAction("Edit", "Home");
            }

            List<Hotel> ListProdHotels = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriHotels = baseUri + "Hotels/";
                Task<String> response = httpClient.GetStringAsync(uriHotels);
                ListProdHotels = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }


            //Liste des chambres demandée par l'utilisateur
            List<Room> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "RoomAdvancedSearch/" + nomlieu + "/" + entre + "/" + sort + "/" + categorie + "/" + wifi + "/" + parking + "/" + type + "/" + prix + "/" + tv + "/" + hairDryer;
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Room>>(response.Result);

            }

            List<Picture> ListProdPictures = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriPictures = baseUri + "PictureAdvancedSearch/" + nomlieu + "/" + entre + "/" + sort + "/" + categorie + "/" + wifi + "/" + parking + "/" + type + "/" + prix + "/" + tv + "/" + hairDryer;
                Task<String> response = httpClient.GetStringAsync(uriPictures);
                ListProdPictures = JsonConvert.DeserializeObject<List<Picture>>(response.Result);
            }

            //Add-on, changement du prix selon l'occupation
            //Nbre de chambres libres dans chaque hôtel :

            //Valais Palace

            List<Room> vs = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                vs = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double nbvalais = 0;
            foreach (Room i in vs)
            {
                nbvalais++;
            }

            //Grand Duc
            List<Room> gd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                gd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbduc = 0;
            foreach (Room i in gd)
            {
                nbduc++;
            }

            //Octodure
            List<Room> oc = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                oc = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nboctodure = 0;
            foreach (Room i in oc)
            {
                nboctodure++;
            }

            //Constantin Palace
            List<Room> co = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                co = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbconstantin = 0;
            foreach (Room i in co)
            {
                nbconstantin++;
            }

            //Walliser Palace
            List<Room> wa = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                wa = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbwalliser = 0;
            foreach (Room i in wa)
            {
                nbwalliser++;
            }

            //Matterhorn
            List<Room> ma = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                ma = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbmatterhorn = 0;
            foreach (Room i in ma)
            {
                nbmatterhorn++;
            }

            // Nbre total de chambres dans chaque hôtel:
            //Valais Palace
            List<Room> val = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                val = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double valais = 0;
            foreach (Room i in val)
            {
                valais++;
            }

            //Grand Duc
            List<Room> grd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                grd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double duc = 0;
            foreach (Room i in grd)
            {
                duc++;
            }

            //Octodure
            List<Room> oct = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                oct = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double octodure = 0;
            foreach (Room i in oct)
            {
                octodure++;
            }

            //Constantin Palace
            List<Room> con = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                con = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double constantin = 0;
            foreach (Room i in con)
            {
                constantin++;
            }

            //Walliser Palace
            List<Room> wal = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                wal = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double walliser = 0;
            foreach (Room i in wal)
            {
                walliser++;
            }

            //Matterhorn
            List<Room> mat = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                mat = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double matterhorn = 0;
            foreach (Room i in mat)
            {
                matterhorn++;
            }

            //Pourcentage de non-occupation
            double valPalace = nbvalais / valais;
            double grandDuc = nbduc / duc;
            double octo = nboctodure / octodure;
            double constPalace = nbconstantin / constantin;
            double mattHorn = nbmatterhorn / matterhorn;
            double wallPalace = nbwalliser / walliser;



            //Test pourcentage
            if (valPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 3)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (grandDuc <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 4)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (octo <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 1)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (constPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 2)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (mattHorn <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 6)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (wallPalace <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (ListProd.ElementAt(i).IdHotel.Id == 5)
                    {
                        ListProd.ElementAt(i).Price = ListProd.ElementAt(i).Price * 120 / 100;
                    }
                }
            }


            MVC.ViewModels.SearchVM form = new ViewModels.SearchVM
            {
                LesHotels = ListProdHotels,
                Chambres = ListProd,
                Picture = ListProdPictures,
                Debut = entre,
                Fin = sort,
                Lieu = nomlieu,
                Categorie = categorie,
                Wifi = wifi,                        //Je souhaite récupérer toutes ces informations pour tester le code rentré par l'utilisateur dans ReservationController
                Parking = parking,
                Type = type,
                Prix = prix,
                TV = tv,
                HairDryer = hairDryer
            };

            return View(form);


        }

        //Résultat de la recherche pour la réservation de groupe
        public ActionResult SearchGroupe(string nomlieu, string entre, string sort, int simple, int doubles)
        {

            DateTime entree = DateTime.Parse(entre);
            DateTime sortie = DateTime.Parse(sort);

            //Si la date d'entrée est avant aujourd'hui
            if (entree < DateTime.Today)
            {
                return RedirectToAction("FormulaireGroupe", "Home");
            }


            //Si la date de sortie est inférieure à la date d'entrée
            if (sortie < entree)
            {
                return RedirectToAction("FormulaireGroupe", "Home");
            }

            //Si les dates sont identiques
            if (sortie == entree)
            {
                return RedirectToAction("FormulaireGroupe", "Home");
            }

            //Rechercher les chambres simples
            List<Room> ListProd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "SimpleRooms/" + nomlieu + "/" + entre + "/" + sort + "/1";
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProd = JsonConvert.DeserializeObject<List<Room>>(response.Result);

            }

            //Rechercher les chambres doubles
            List<Room> ListProdD = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uri = baseUri + "DoubleRooms/" + nomlieu + "/" + entre + "/" + sort + "/2";
                Task<String> response = httpClient.GetStringAsync(uri);
                ListProdD = JsonConvert.DeserializeObject<List<Room>>(response.Result);

            }

            List<Hotel> ListProdHotels = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriHotels = baseUri + "Hotels/";
                Task<String> response = httpClient.GetStringAsync(uriHotels);
                ListProdHotels = JsonConvert.DeserializeObject<List<Hotel>>(response.Result);
            }

            List<Picture> ListProdPictures = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string uriPictures = baseUri + "PictureSearch/" + nomlieu + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uriPictures);
                ListProdPictures = JsonConvert.DeserializeObject<List<Picture>>(response.Result);
            }

            List<Room> items = new List<Room>();

            //Plusieurs tests pour savoir si le nombre de chambres demandées est trop grand par rapport à ce qui existe
            int nombreChambresSimples = 0;

            for (int n = 0; n < ListProd.Count(); n++)
            {
                nombreChambresSimples++;
            }

            int nombreChambresDoubles = 0;

            for (int p = 0; p < ListProdD.Count(); p++)
            {
                nombreChambresDoubles++;
            }

            if (simple > nombreChambresSimples)
            {
                for (int i = 0; i < nombreChambresSimples; i++)
                {
                    items.Add(ListProd.ElementAt(i));
                }
            }
            else
            {
                for (int i = 0; i < simple; i++)
                {
                    items.Add(ListProd.ElementAt(i));
                }
            }
            if (doubles > nombreChambresDoubles)
            {
                for (int i = 0; i < nombreChambresDoubles; i++)
                {
                    items.Add(ListProdD.ElementAt(i));
                }
            }
            else
            {
                for (int i = 0; i < doubles; i++)
                {
                    items.Add(ListProdD.ElementAt(i));
                }
            }
           
            //Add-on, changement du prix selon l'occupation
            //Nbre de chambres libres dans chaque hôtel :

            //Valais Palace

            List<Room> vs = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                vs = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double nbvalais = 0;
            foreach (Room i in vs)
            {
                nbvalais++;
            }

            //Grand Duc
            List<Room> gd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                gd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbduc = 0;
            foreach (Room i in gd)
            {
                nbduc++;
            }

            //Octodure
            List<Room> oc = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                oc = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nboctodure = 0;
            foreach (Room i in oc)
            {
                nboctodure++;
            }

            //Constantin Palace
            List<Room> co = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                co = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbconstantin = 0;
            foreach (Room i in co)
            {
                nbconstantin++;
            }

            //Walliser Palace
            List<Room> wa = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                wa = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbwalliser = 0;
            foreach (Room i in wa)
            {
                nbwalliser++;
            }

            //Matterhorn
            List<Room> ma = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel + "/" + entre + "/" + sort;
                Task<String> response = httpClient.GetStringAsync(uri);
                ma = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double nbmatterhorn = 0;
            foreach (Room i in ma)
            {
                nbmatterhorn++;
            }

            // Nbre total de chambres dans chaque hôtel:
            //Valais Palace
            List<Room> val = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Valais Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                val = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }
            double valais = 0;
            foreach (Room i in val)
            {
                valais++;
            }

            //Grand Duc
            List<Room> grd = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Grand Duc";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                grd = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double duc = 0;
            foreach (Room i in grd)
            {
                duc++;
            }

            //Octodure
            List<Room> oct = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Octodure";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                oct = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double octodure = 0;
            foreach (Room i in oct)
            {
                octodure++;
            }

            //Constantin Palace
            List<Room> con = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Constantin Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                con = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double constantin = 0;
            foreach (Room i in con)
            {
                constantin++;
            }

            //Walliser Palace
            List<Room> wal = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Walliser Palace";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                wal = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double walliser = 0;
            foreach (Room i in wal)
            {
                walliser++;
            }

            //Matterhorn
            List<Room> mat = null;
            using (HttpClient httpClient = new HttpClient())
            {
                string nomHotel = "Matterhorn";
                string uri = baseUri + "RoomHotelName/" + nomHotel;
                Task<String> response = httpClient.GetStringAsync(uri);
                mat = JsonConvert.DeserializeObject<List<Room>>(response.Result);
            }

            double matterhorn = 0;
            foreach (Room i in mat)
            {
                matterhorn++;
            }

            //Pourcentage de non-occupation
            double valPalace = nbvalais / valais;
            double grandDuc = nbduc / duc;
            double octo = nboctodure / octodure;
            double constPalace = nbconstantin / constantin;
            double mattHorn = nbmatterhorn / matterhorn;
            double wallPalace = nbwalliser / walliser;



            //Test pourcentage
            if (valPalace <= 0.3)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 3)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (grandDuc <= 0.3)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 4)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (octo <= 0.3)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 1)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (constPalace <= 0.3)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 2)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (mattHorn <= 0.3)
            {
                for (int i = 0; i < ListProd.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 6)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }
            if (wallPalace <= 0.3)
            {
                for (int i = 0; i < items.Count(); i++)
                {
                    if (items.ElementAt(i).IdHotel.Id == 5)
                    {
                        items.ElementAt(i).Price = items.ElementAt(i).Price * 120 / 100;
                    }
                }
            }

            decimal price = 0;
            foreach (Room i in items)
            {
                price += i.Price;
            }

            MVC.ViewModels.SearchVM formulaire = new ViewModels.SearchVM
            {
                Price = price,
                Chambres = items,
                LesHotels = ListProdHotels,
                Picture = ListProdPictures,
                Debut = entre,
                Fin = sort
            };


            return View(formulaire);
        }

    }
}