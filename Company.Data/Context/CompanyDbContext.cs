using Company.Data.Entites;
using Microsoft.AspNet.Identity.EntityFramework;
using Company.Data.Entites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Company.Data.Context
{
    public class CompanyDbContext :IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Employee>employees { get; set; }
        public DbSet<Department>departments { get; set; }


    }
}
