using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Models
{
    public class Pojistka
    {
        [Key]
        public int PojisteniId { get; set; }
        //[Required]
        //[MaxLength(50)]
        //[DisplayName("Typ")]
        //public Kategorie Kategorie { get; set; } //propojení s jiným modelem!
        [Required]
        [Display(Name = "Podmínky")]
        [MaxLength(1000)]
        public string Podminky { get; set; }
        //[Required]
        //[Display(Name = "Záloha")]
        //[Range(1, double.MaxValue, ErrorMessage = "Částka musí být větší než 0")]
        //public Double Zaloha { get; set; }
        //[Display(Name = "Obrázek - url")]
        //public string ImageUrl { get; set; }
    }
}
