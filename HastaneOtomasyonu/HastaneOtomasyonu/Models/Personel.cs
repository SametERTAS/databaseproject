using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class Personel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public string pozisyon { get; set; }
        [ForeignKey("id")]
        public Kisi kisi { get; set; }
        public int hastaneKlinikId { get; set; }
        [ForeignKey("hastaneKlinikId")]
        public HastaneKlinik hastaneKlinik { get; set; }
    }
}
