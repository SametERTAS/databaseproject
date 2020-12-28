using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Sehir
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }


        [Display(Name = "Şehir Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [Index(IsUnique = true)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} alanı en az {2}, en çok {1} karakter olmalıdır")]
       
        public string adi { get; set; }


        [Index(IsUnique =true)]
        [Display(Name = "Plaka Kodu")]
        public int? plakaKodu { get; set; }



        [Index(IsUnique = true)]
        [Display(Name = "Telefon Kodu")]
        public int? telefonKodu { get; set; }





        [Display(Name ="Ülke Adı")]
        public int ulkeId { get; set; }
        [ForeignKey("ulkeId")]
        [Display(Name ="Ülke")]
        public Ulke ulke { get; set; }


    }
}
