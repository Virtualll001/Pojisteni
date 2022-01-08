using Pojisteni.DataAccess.Data;
using Pojisteni.DataAccess.Repository.IRepository;
using Pojisteni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.DataAccess.Repository
{
    public class PojistkaRepository : Repository<Pojistka>, IPojistkaRepository
    {
        private readonly ApplicationDbContext _db;
        public PojistkaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Pojistka pojistka)
        {
            var objFromDb = _db.Pojistky.FirstOrDefault(s => s.PojisteniId == pojistka.PojisteniId);               
            if (objFromDb != null)
            {
                //obrázek vyžaduje speciální kontrolu!
                if (pojistka.ImageUrl != null)
                {
                    objFromDb.ImageUrl = pojistka.ImageUrl;
                }
                objFromDb.Nazev = pojistka.Nazev;
                objFromDb.Podminky = pojistka.Podminky;
                objFromDb.Zaloha = pojistka.Zaloha; 
                objFromDb.Kategorie = pojistka.Kategorie;                             
            }
        }
    }
}

