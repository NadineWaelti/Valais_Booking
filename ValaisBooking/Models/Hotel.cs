﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValaisBooking.Models
{
    public class Hotel
    {   public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Category { get; set; }
        public bool HasWifi { get; set; }
        public bool HasParking { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Photo { get; set; }
    }
}