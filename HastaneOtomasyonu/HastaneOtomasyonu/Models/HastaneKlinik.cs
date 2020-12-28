using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class HastaneKlinik
    {
        [Key]
        public int id { get; set; }

        public int hastaneId { get; set; }
        public int klinikId { get; set; }

        [ForeignKey("hastaneId")]
        public Hastane hastane { get; set; }
        [ForeignKey("klinikId")]
        public Klinik klinik { get; set; }
    }
}
