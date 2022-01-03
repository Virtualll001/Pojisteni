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
    public class KategorieRepository : Repository<Kategorie>, IKategorieRepository
    {
        private readonly ApplicationDbContext _db;
        public KategorieRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Kategorie kategorie)
        {
            var objFromDb = _db.Kategories.FirstOrDefault(s => s.KategorieId == kategorie.KategorieId);
            if(objFromDb != null)
            {
                objFromDb.Typ = kategorie.Typ;               
            }            
        }
    }   
}
