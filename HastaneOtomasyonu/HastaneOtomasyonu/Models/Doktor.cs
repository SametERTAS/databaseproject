using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Doktor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Doktor Seç")]
        public int id { get; set; }
        [Display(Name ="Ünvan")]
        public string unvan { get; set; }
        [Display(Name ="Klinik Seç")]



        public int hastaneKlinikId { get; set; }
        [ForeignKey("hastaneKlinikId")]
        public HastaneKlinik hastaneKlinik { get; set; }



        [ForeignKey("id")]
        public Kisi kisi { get; set; }
    }
}
