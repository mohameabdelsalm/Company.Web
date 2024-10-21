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

            var mappingDepartment = _mapper.Map<Department>(departmentDto);

            _unitOfWork.DepartmentRepository.Add(mappingDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            Department mappingDepartment = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(mappingDepartment);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
           
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var Mapdept = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Mapdept;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id == null)
                return null;

            var departmet = _unitOfWork.DepartmentRepository.GetById(id.Value);

            if (departmet is null)
            
                return null;
            var mapdepartment=_mapper.Map<DepartmentDto>(departmet);

            return mapdepartment;
        }

        public void Update(DepartmentDto department)
        {
            //_unitOfWork.DepartmentRepository.Update(department);
            //_unitOfWork.Complete();
        }
    }
}
