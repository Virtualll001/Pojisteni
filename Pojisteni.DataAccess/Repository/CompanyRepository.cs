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
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Update(company);

            //var objFromDb = _db.Companies.FirstOrDefault(s => s.Id == company.Id);
            //if(objFromDb != null)
            //{
            //    //objFromDb.Typ = company.Typ;               
            //}            
        }
    }   
}
