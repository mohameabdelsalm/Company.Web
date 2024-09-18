using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public void Add(Department department)
        {
            var mappingDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = DateTime.Now,
            };
            _departmentRepository.Add(mappingDepartment);
        }

        public void Delete(Department department)
        {
            _departmentRepository.Delete(department);
        }

        public IEnumerable<Department> GetAll()
        {
           var departments = _departmentRepository.GetAll().Where(x=>x.IsDeleted==false);
            return departments;
        }

        public Department GetById(int? id)
        {
            if (id == null)
                return null;

            var departmet = _departmentRepository.GetById(id.Value);

            if (departmet is null)
            {
                return null;
            }
            return departmet;
        }

        public void Update(Department department)
        {
            _departmentRepository.Update(department);
        }
    }
}
