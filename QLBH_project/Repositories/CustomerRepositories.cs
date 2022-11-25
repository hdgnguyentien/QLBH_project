using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class CustomerRepositories : ICustomerRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public CustomerRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addcustomers(customers customers)
        {
            try
            {
                cuaHangDbContext.customers.Add(customers);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<customers> GetAll()
        {
            return cuaHangDbContext.customers.ToList();
        }

        public customers GetByID(Guid id)
        {
            var customer = cuaHangDbContext.customers.FirstOrDefault(p => p.Id == id);
            return customer;
        }

        public bool Removecustomers(customers customers)
        {
            try
            {
                cuaHangDbContext.customers.Remove(customers);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updatecustomers(customers customers)
        {
            try
            {
                cuaHangDbContext.customers.Update(customers);
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
