using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interface
{
    public interface IDepartmentService
    {
        Department GetById(int id);
        IEnumerable<Department> GetAll();
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
