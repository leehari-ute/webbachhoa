using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int IDCart { get; set; }
        public int IDuser { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public double TotalPrice { get { return Amount * Price; } }
    }
}