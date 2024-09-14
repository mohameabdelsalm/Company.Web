using Company.Data.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Configration
{
    public class DepartmentConfigrations : IEntityTypeConfiguration<Department>
    {
        void IEntityTypeConfiguration<Department>.Configure(EntityTypeBuilder<Department> builder)
        {
           builder.Property(x=>x.ID).UseIdentityColumn(10,10);
            builder.HasIndex(x => x.Name).IsUnique(true);
        }
    }
}
