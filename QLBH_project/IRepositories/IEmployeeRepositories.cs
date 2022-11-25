using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IEmployeeRepositories
    {
        IEnumerable<employees> GetAll();
        employees GetByID(Guid id);
        public bool Addemployees(employees employees);
        public bool Updateemployees(employees employees);
        public bool Removeemployees(employees employees);
    }
}
