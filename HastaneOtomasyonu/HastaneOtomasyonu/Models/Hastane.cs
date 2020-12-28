using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Hastane
    {
        [Key]
        public int id { get; set; }



        [Required]
        [Display(Name = "Hastane Adı")]
        public string adi { get; set; }


        [Display(Name = "Telefon Numarası")]
        public string telefonNo { get; set; }


        [Display(Name = "İlçe Adı")]
        public int? ilceId { get; set; }
        [ForeignKey("ilceId")]
        [Display(Name = "İlçe")]
        public Ilce ilce { get; set; }
    }
}
