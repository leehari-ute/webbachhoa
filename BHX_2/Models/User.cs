using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int idUser { get; set; }
        [Key, Column(Order = 2)]

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        
        public int Phone { get; set; }

        public int Lever { get; set; }
        public string HoTen { get; set; }
        public DateTime ngaySinh { get; set; }
        public string DiaChi { get; set; }
    }

}