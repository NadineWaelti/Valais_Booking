using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public decimal Price { get; set; }
        public bool HasTV { get; set; }
        public bool HasHairDryer { get; set; }
        public Hotel IdHotel { get; set; }
    }
}