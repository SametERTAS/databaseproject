using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Models
{
    public enum Cinsiyet
    {
        Erkek = 1,
        Kadın = 2
    }
    public enum MedeniDurum
    {
        Bekar = 1,
        Boşanmış = 2,
        Evli = 3
    }
    public class Kisi
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Adı")]
        public string adi { get; set; }
        [Display(Name = "Soyadı")]
        public string soyadi { get; set; }

        public string tamIsim
        {
            get
            {
                return adi + " " + soyadi;
            }
        }


        [Display(Name = "Adres")]
        public string adres { get; set; }
        [Display(Name = "Cep Telefonu")]
        public string cepNo { get; set; }
        [Display(Name = "Ev Telefonu")]
        public string evNo { get; set; }
        [Display(Name = "İş Telefonu")]
        public string isNo { get; set; }

        [Index(IsUnique = true)]
        [Display(Name = "Kimlik Numarası")]
        public string TCNo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime dogumTarihi { get; set; }
        [Display(Name = "Cinsiyet")]
        public Cinsiyet cinsiyet { get; set; }
        [Display(Name = "Medeni Durum")]
        public MedeniDurum medeniDurum { get; set; }

         public int sehirId { get; set; }
          public int kanGrubuId { get; set; }


        [Display(Name = "Kişi Türü")]
        public char kisiTuru { get; set; }





        [ForeignKey("sehirId")]
        [Display(Name = "Şehir")]
        public Sehir sehir { get; set; }



        [ForeignKey("kanGrubuId")]
        public KanGrubu kanGrubu { get; set; }




        public Personel Personel { get; set; }
        public Hasta Hasta { get; set; }
        public Doktor Doktor { get; set; }
    }
}
