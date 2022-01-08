using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.Models.ViewModels
{
    public class PojistkaVM
    {
        public Pojistka Pojistka { get; set; }        
        public IEnumerable<SelectListItem> TypSeznam { get; set; } //nazev musí být originál! Ne "Typ" - už existuje...
    }
}
