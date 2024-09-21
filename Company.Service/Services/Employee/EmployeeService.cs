using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Repository.Repository;
using Company.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Employee employee)
        {
            
        }

        public void Delete(Employee employee)
        {
           _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return employees;
        }

        public Employee GetById(int? id)
        {

            if (id == null)
                return null;

            var employees = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employees is null)
            {
                return null;
            }
            return employees;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        
          => _unitOfWork.EmployeeRepository.GetEmployeeByName(name);

        

        public void Update(Employee employee)
        {
           _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
