using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Ilce
    {
        public int id { get; set; }

        [Display(Name = "İlçe Adı")]
        [Index(IsUnique =true)]

        public string adi { get; set; }
        [Index(IsUnique =true)]
        [Display(Name = "Telefon Kodu")]
        public int? telefonKodu { get; set; }




        [Display(Name ="Şehir Adı")]
        public int sehirId { get; set; }
        [ForeignKey("sehirId")]
        [Display(Name = "Şehir")]
        public Sehir sehir { get; set; }
    }
}
