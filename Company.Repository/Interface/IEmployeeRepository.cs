using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interface
{
    public interface IEmployeeRepository :IGenricRepository<Employee> 
    {
       IEnumerable<Employee> GetEmployeeByName(string name);
      IEnumerable<Employee>GetEmployeeByAdress(string address);

        
    }
}
