using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoviesCF_Odata.Models
{ [Table("Flicks")]
    public class Flicks
    {[Key]
        public int FId { get; set; }
        public string FName { get; set; }
        public string Starcast { get; set; }
        public string Producer { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}