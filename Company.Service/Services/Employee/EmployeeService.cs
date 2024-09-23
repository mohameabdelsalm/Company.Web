using AutoMapper;
using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Service.Dto;
using Company.Service.Helper;
using Company.Service.Interface;
using Company.Service.Mapping;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");

             Employee employee = _mapper.Map<Employee>(employeeDto);

            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            IEnumerable<EmployeeDto> MapEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

           
            return MapEmployees;
        }

        public EmployeeDto GetById(int? id)
        {

            if (id == null)
                return null;

            var employees = _unitOfWork.EmployeeRepository.GetById(id.Value);

            if (employees is null)
            
             return null;
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employees);
            
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
            IEnumerable<EmployeeDto> MapEmployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return MapEmployees;
        }




        public void Update(EmployeeDto employee)
        {

           
        }
    }
}
