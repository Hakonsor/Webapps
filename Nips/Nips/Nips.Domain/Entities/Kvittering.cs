using System.ComponentModel.DataAnnotations;
using System;

namespace Nips.Model.Entities
{
    public class Kvittering
    {
        //BETALING
        [Key]
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ\s]+$)", ErrorMessage = "Ugyldig fornavn")]
        public String firstname { get; set; }
        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ\s]+$)", ErrorMessage = "Ugyldig etternavn")]
        public String lastname { get; set; }
        [Display(Name = "Kortnummer")]
        [RegularExpression(@"(^[0-9\+\(\)\s]{16,}$)", ErrorMessage = "Ugyldig kortnr")]
        [Required(ErrorMessage = "Kortnummer må oppgis")]
        public String kortnr { get; set; }
        [Display(Name = "Sikkerhetskode")]
        [RegularExpression(@"(^[0-9\+\(\)\s]{3,}$)", ErrorMessage = "Ugyldig CVV")]
        [Required(ErrorMessage = "CVV må oppgis")]
        public String cvv { get; set; }
        [Display(Name = "MM")]
        [RegularExpression(@"(^[0-9\+\(\)\s]{2,}$)", ErrorMessage = "Ugyldig MM")]
        [Required(ErrorMessage = "MM må oppgis")]
        public String mm { get; set; }
        [Display(Name = "YY")]
        [RegularExpression(@"(^[0-9\+\(\)\s]{2,}$)", ErrorMessage = "Ugyldig YY")]
        [Required(ErrorMessage = "YY må oppgis")]
        public String yy { get; set; }

        //KUNDEN
        public Kunde kunde { get; set; }
        public Order order { get; set; }
        public Handlevogn handlevogn { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double exmva { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double mva { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public int sum { get; set; }
    }
}

