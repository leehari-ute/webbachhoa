using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class Water
    {
        public int WaterID { get; set; }
        public string WaterName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public float Price { get; set; }
        public byte[] Image { get; set; }
        public string UrlImage { get; set; }
    }
}