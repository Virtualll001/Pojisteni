using Pojisteni.DataAccess.Data;
using Pojisteni.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            SP_Call = new SP_Call(_db);
            Kategorie = new KategorieRepository(_db);
            Pojistka = new PojistkaRepository(_db);
            Pojistnik = new PojistnikRepository(_db);
        }

        public ISP_Call SP_Call { get; private set; }
        public IKategorieRepository Kategorie { get; private set; } 
        public IPojistkaRepository Pojistka { get; private set; }
        public IPojistnikRepository Pojistnik { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
