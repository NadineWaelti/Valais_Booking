using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValaisBooking.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Fin { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }
        public decimal Price { get; set; }

    }
}