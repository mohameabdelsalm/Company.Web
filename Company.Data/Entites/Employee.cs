using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entites
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        [MaxLength(11)]
        public int PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

    }
}
