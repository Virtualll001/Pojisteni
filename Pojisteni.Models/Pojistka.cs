using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Models
{
    public class Pojistka
    {
        [Key]
        public int PojisteniId { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím název pojistky")]
        [Display(Name = "Název")]
        [MaxLength(50)]
        public string Nazev { get; set; }

        [Required(ErrorMessage = "Vyplňte prosím smluvní podmínky")]
        [Display(Name = "Podmínky")]
        [MaxLength(1000)]
        public string Podminky { get; set; }

        //typ double pro peněžní částky!
        [Required(ErrorMessage = "Zadejte prosím výši zálohy")]
        [Display(Name = "Záloha")]
        [Range(1, 10000, ErrorMessage = "Částka musí být v rozmezí 1,- až 10.000,-")]
        public double Zaloha { get; set; }

        //obrázek!
        [Required(ErrorMessage = "Přiložte prosím obrázek")]
        [Display(Name = "Ilustrace")]
        public string ImageUrl { get; set; }

        [Display(Name = "Kategorie")]
        public int KategorieId { get; set; }

        
        [ForeignKey("KategorieId")]
        [Required(ErrorMessage = "Vyplňte typ pojištění!")]
        [MaxLength(50)]
        [Display(Name = "Typ pojištění")]
        public Kategorie Kategorie { get; set; }
    }   
}
