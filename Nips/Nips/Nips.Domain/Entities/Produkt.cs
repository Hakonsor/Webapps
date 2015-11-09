using System;
using System.ComponentModel.DataAnnotations;

namespace Nips.Model.Entities
{
    public class Produkt
    {
 

        [Display(Name = "id")]
        public int id { get; set; }

        [Display(Name = "Varenavn")]
        [Required(ErrorMessage = "Varenavn må oppgis")]
        public string navn { get; set; }

        [Display(Name = "Beskrivelse")]
        public string beskrivelse { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Pris må oppgis")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Pris kan bare bestå av heltall")]
        public int pris { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Antall varer kan kun bestå av heltall")]
        [Display(Name = "Antall")]
        public int antall { get; set; }

        [Display(Name = "Kategori")]
        public String kategori { get; set; }
    

        public string ImageUrl { get; set; }

    }
}