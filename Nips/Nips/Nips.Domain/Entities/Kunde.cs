using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nips.Model.Entities
{
    public class Kunde
    {
        [Display(Name ="Kunde Id")]
        public int kundeid { get; set; }
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ\s]+$)", ErrorMessage = "Ugyldig fornavn")]
        public String fornavn { get; set; }
        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ\s]+$)", ErrorMessage = "Ugyldig etternavn")]
        public String etternavn { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]@[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$)", ErrorMessage = "Ugyldig email")]
        public String email { get; set; }
        [Display(Name = "Telefonnummer")]
        [RegularExpression(@"(^[0-9\+\(\)\s]{8,}$)", ErrorMessage = "Ugyldig telefonnummer")]
        [Required(ErrorMessage = "Telefonnummer må oppgis")]
        public String tlf { get; set; }
        [Display(Name = "Adresse")]
        [RegularExpression(@"(^[a-zA-Z0-9æÆøØåÅ\s]+$)", ErrorMessage = "Ugyldig adresse")]
        [Required(ErrorMessage = "Adresse må oppgis")]
        public String adresse { get; set; }
        [Display(Name = "Postnummer")]
        [RegularExpression(@"(^[0-9]{4})", ErrorMessage = "Ugyldig postnummer")]
        [Required(ErrorMessage = "Postnummer må oppgis")]
        public String postnr { get; set; }
        [Display(Name = "Poststed")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ]+$)", ErrorMessage = "Ugyldig poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public String poststed { get; set; }
        [Display(Name = "Passord")]
        [RegularExpression(@"(^[a-zA-Z0-9æÆøØåÅ]{8,}$)", ErrorMessage = "Ugyldig passord, må ha minst 8 tegn")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public String passord { get; set; }
        public byte[] hashpassord { get; set; }
        [Display(Name ="Admin")]
        public bool admin { get; set; }
        public Handlevogn handlevogn { get; set; }
        public List<Order> Order { get; set; }
    }
}