using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Models
{
    public class Kategorie
    {
        [Key]
        public int KategorieId { get; set; }
        [Required(ErrorMessage = "Vyplňte typ pojištění!")]
        [MaxLength(50)]       
        [Display(Name = "Typ pojištění")]
        public string Typ { get; set; }
    }
}
