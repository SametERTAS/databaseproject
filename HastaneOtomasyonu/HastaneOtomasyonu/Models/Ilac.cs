using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Ilac
    {
        [Key]
        public string ilacKodu { get; set; }
        [Required]
        [Index(IsUnique = true)]
        public string ilacAdi { get; set; }
        public string ilacMarkasi { get; set; }
        public double fiyat { get; set; }
    }
}
