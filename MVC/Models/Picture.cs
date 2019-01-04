using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Room IdRoom { get; set; }
    }
}