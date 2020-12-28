using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Klinik
    {
        public int id { get; set; }



        [Required]
        [Display(Name = "Klinik")]
        public string adi { get; set; }

      
    }
}
