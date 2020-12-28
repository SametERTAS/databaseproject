using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Ulke
    {
        [Key]
        public int id { get; set; }



        [Required]
        [Index(IsUnique = true)]
        [Display(Name ="Ülke Adı")]
        [StringLength(50,MinimumLength =5, ErrorMessage ="{0} en fazla {1} en az {2} karakter olmalıdır")]
        public string adi { get; set; }





        [Index(IsUnique = true)]
        [Display(Name ="Ülke Kodu")]
        [Range(0,1000,ErrorMessage ="Lütfen {0} alanı için {1} ve {2} değerleri arasında bir sayı giriniz")]
        public int? ulkeKodu { get; set; }
    }
}
