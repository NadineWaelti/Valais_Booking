using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class OverviewVM
    {
        public List<Reservation> Reservations { get; set; }
        public List<Lien> Liens { get; set; }
    }
}