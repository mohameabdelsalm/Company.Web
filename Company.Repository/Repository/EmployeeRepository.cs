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
    public class EmployeeRepository : GenricRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        => _context.employees.Where(x => 
        x.Name.Trim().ToLower().Contains(name.Trim().ToLower())||
        x.Email.Trim().ToLower().Contains(name.Trim().ToLower())||
        x.PhoneNumber.Trim().ToLower().Contains(name.Trim().ToLower())
        ).ToList();

        public IEnumerable<Employee> GetEmployeeByAdress(string address)
        {
            throw new NotImplementedException();
        }

      
    }
}
