using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public class MuayeneTest
    {
        public int id { get; set; }
        public int muayeneId { get; set; }
        public int testId { get; set; }
        [ForeignKey("muayeneId")]
        public Muayene muayene { get; set; }
        [ForeignKey("testId")]
        public Test test { get; set; }
    }
}
