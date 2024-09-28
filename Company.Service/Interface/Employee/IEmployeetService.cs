using Company.Data.Entites;
using Company.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interface
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int? id);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto employee);
        void Update(EmployeeDto employee);
        void Delete(EmployeeDto employee);
        IEnumerable<EmployeeDto> GetEmployeeByName(string name);
        //void Add(Department department);
    }
}
