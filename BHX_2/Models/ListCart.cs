using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class ListCart
    {
        [Key]
        public int IDCart { get; set; }
        public double TotalPrice { get; set; }
        public int IDuser { get; set; }
        public string Status { get; set; }
    }
}