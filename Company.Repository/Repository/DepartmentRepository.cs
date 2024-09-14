using Company.Data.Context;
using Company.Data.Entites;
using Company.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class DepartmentRepository :GenricRepository<Department>,IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context):base(context)
        {
            _context = context;
        }
       
    }
}
