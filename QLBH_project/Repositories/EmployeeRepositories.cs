using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class EmployeeRepositories : IEmployeeRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public EmployeeRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addemployees(employees employees)
        {
            try
            {
                cuaHangDbContext.employees.Add(employees);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<employees> GetAll()
        {
            var employees = cuaHangDbContext.employees.Include(x => x.roles).ToList();
            return employees;
        }

        public employees GetByID(Guid id)
        {
            return cuaHangDbContext.employees.FirstOrDefault(p => p.Id == id);
        }
        public bool Removeemployees(employees employees)
        {
            try
            {
                cuaHangDbContext.employees.Remove(employees);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateemployees(employees employees)
        {
            try
            {
                cuaHangDbContext.employees.Update(employees);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
