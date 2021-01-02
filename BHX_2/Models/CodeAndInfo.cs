using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BHX_2.Models
{
    public class CodeAndInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        public int code { get; set; }
        public string newPass { get; set; }
        public string newMail { get; set; }
        public string newPhone { get; set; }
        public DateTime newBirth { get; set; }
        public string newAdd { get; set; }
        public string newName { get; set; }
    }
}