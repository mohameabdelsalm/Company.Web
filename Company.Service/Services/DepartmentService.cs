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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Department department)
        {
            var mappingDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = DateTime.Now,
            };
            _unitOfWork.DepartmentRepository.Add(mappingDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
           var departments = _unitOfWork.DepartmentRepository.GetAll().Where(x=>x.IsDeleted==false);
            return departments;
        }

        public Department GetById(int? id)
        {
            if (id == null)
                return null;

            var departmet = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (departmet is null)
            {
                return null;
            }
            return departmet;
        }

        public void Update(Department department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
