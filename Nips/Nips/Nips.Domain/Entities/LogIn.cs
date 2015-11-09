using System;
using System.ComponentModel.DataAnnotations;
namespace Nips.Domain.Entities
{
    public class LogIn
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email må oppgis")]
        [RegularExpression(@"(^[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]@[a-zA-ZæÆøØåÅ][\w\.-]*[a-zA-Z0-9æÆøØåÅ]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$)", ErrorMessage = "Ugyldig email")]
        public String email { get; set; }
        [Required(ErrorMessage = "Passord må oppgis")]
        public String passord { get; set; }
    }
}
