using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Lien
    {
        public int Id { get; set; }
        public Reservation IdReservation { get; set; }
        public Room IdRoom { get; set; }
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
    }
}