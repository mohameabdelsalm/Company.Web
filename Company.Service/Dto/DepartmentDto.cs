using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Dto
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreateAt { get; set; }
        public ICollection<EmployeeDto> Employees { get; set; } = new HashSet<EmployeeDto>();
    }
}
