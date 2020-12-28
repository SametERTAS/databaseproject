using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Randevu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime saat { get; set; }
        public int hastaneKlinikId { get; set; }
        public int hastaId { get; set; }
        public int doktorId { get; set; }

        [ForeignKey("hastaneKlinikId")]
        public HastaneKlinik  hastaneKlinik { get; set; }
        [ForeignKey("hastaId")]
        public Hasta hasta { get; set; }
        [ForeignKey("doktorId")]
        public Doktor doktor { get; set; }

        public Muayene muayene { get; set; }
    }
}
