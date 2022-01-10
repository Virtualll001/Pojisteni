using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Models
{
    public class Company
    {        
        public int Id { get; set; } 
        [Required(ErrorMessage = "Vyplňte prosím název společnosti")]
        [Display(Name = "Jméno společnosti")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Display(Name = "Adresa")]
        [MaxLength(500)]
        public string StreetAddress { get; set; }
        [Display(Name = "Město")]
        [MaxLength(50)]
        public string City { get; set; }
        [Display(Name = "PSČ")]
        [MaxLength(10)]
        public string PostalCode { get; set; }        
        [Display(Name = "Telefonní číslo")]
        [RegularExpression("^(\\+420)? ?[1-9][0-9]{2} ?[0-9]{3} ?[0-9]{3}$", ErrorMessage = "Zadejte prosím platné telefonní číslo")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Autorizace")]
        public bool IsAuthorizedCompany { get; set; }        

    }
}
