using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class ToutesMesListes
    {
        public List<Room> Chambres { get; set; }
        public List<Hotel> LesHotels { get; set; }
        public List<Picture> Picture { get; set; }
        public List<Lien> Lien { get; set; }
        public List<Reservation> Reservation { get; set; }
    }
}