using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Recete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string receteNo { get; set; }
        public DateTime tarih { get; set; }
        public int muayeneId { get; set; }
       
        [ForeignKey("muayeneId")]
        public Muayene muayene { get; set; }
       
    }
}
