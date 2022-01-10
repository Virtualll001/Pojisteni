﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IKategorieRepository Kategorie { get; }
        IPojistkaRepository Pojistka { get; }
        IPojistnikRepository Pojistnik { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
