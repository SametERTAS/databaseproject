using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class ReceteIlac
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string receteNo { get; set; }
        public string ilacKodu { get; set; }

        [ForeignKey("receteNo")]
        public Recete recete { get; set; }
        [ForeignKey("ilacKodu")]
        public Ilac ilac { get; set; }
    }
}
