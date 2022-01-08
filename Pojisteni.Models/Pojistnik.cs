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
    public class Pojistnik
    {
        [Key]
        public int PojistnikId { get; set; }
        [Required(ErrorMessage ="Zadejte prosím jméno")]
        [StringLength(50)]
        [DisplayName("Jméno")]
        public string Jmeno { get; set; }
        [Required(ErrorMessage = "Zadejte prosím příjmení")]
        [StringLength(50)]
        [DisplayName("Příjmení")]
        public string Prijmeni { get; set; }
        [Required(ErrorMessage = "Zadejte prosím adresu")]
        [StringLength(1000)]
        public string Adresa { get; set; }
        [Required(ErrorMessage ="Zadejte prosím telefonní číslo")]
        [DisplayName("Telefonní číslo")]
        [RegularExpression("^(\\+420)? ?[1-9][0-9]{2} ?[0-9]{3} ?[0-9]{3}$", ErrorMessage = "Zadejte prosím platné telefonní číslo")]
        public string TelefonCislo { get; set; }

        //propojení s tabulkou pojištění (tabulka Událost je propojená s tabulkou pojištění)        
        public int PojisteniId { get; set; }

        [ForeignKey("PojisteniId")]
        [Required(ErrorMessage = "Vyplňte název pojistky!")]
        [MaxLength(50)]
        [Display(Name = "Pojištění")]
        public Pojistka Pojistka { get; set; }
    }
}
