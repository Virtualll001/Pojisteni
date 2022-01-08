using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pojisteni.Models;

namespace Pojisteni.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Kategorie> Kategories { get; set; }
        public DbSet<Pojistka> Pojistky { get; set; }
        public DbSet<Pojistnik> Pojistnici { get; set; }        

    }
}