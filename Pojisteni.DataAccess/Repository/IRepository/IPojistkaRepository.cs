using Pojisteni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.DataAccess.Repository.IRepository
{
    public interface IPojistkaRepository : IRepository<Pojistka>
    {
        void Update(Pojistka pojistka);
    }
}
