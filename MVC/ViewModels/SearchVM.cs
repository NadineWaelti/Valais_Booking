using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class SearchVM
    {
        public List<Room> Chambres { get; set; }
        public Hotel Hotel { get; set; }
        public List<Hotel> LesHotels { get; set; }
        public List<Picture> Picture { get; set; }
        public List<Reservation> Reservation { get; set; }
        public Reservation ReservationObjet { get; set; }
        public string Lieu { get; set; }
        public int Categorie { get; set; }
        public bool Wifi { get; set; }
        public bool Parking { get; set; }
        public int Type { get; set; }
        public decimal Prix { get; set; }
        public bool TV { get; set; }
        public bool HairDryer { get; set; }
        public string Debut { get; set; }
        public string Fin { get; set; }
        public decimal Price { get; set; }
    }
}