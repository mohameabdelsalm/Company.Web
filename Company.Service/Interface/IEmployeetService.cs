﻿using Company.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interface
{
    public interface IEmployeetService
    {
        Employee GetById(int? id);
        IEnumerable<Employee> GetAll();
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        IEnumerable<Employee> GetEmployeeByName(string name);
        void Add(Department department);
    }
}
