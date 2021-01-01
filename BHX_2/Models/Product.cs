using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductGroup { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }       
        public byte[] Image { get; set; }
        public string UrlImage { get; set; }
    }
}