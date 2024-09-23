using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Service.Dto;
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
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
           _mapper = mapper;
        }
        public void Add(DepartmentDto departmentDto)
        {

            //Department mappingDepartment = _mapper.Map<Department>(departmentDto);
            
            _unitOfWork.DepartmentRepository.Add(departmentDto);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            //Department mappingDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(departmentDto);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
           
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            //IEnumerable < DepartmentDto > Mapdept=_mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departments;
        }

        public DepartmentDto GetById(int? id)
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

        public void Update(DepartmentDto department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
