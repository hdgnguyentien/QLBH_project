using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class OrderRepositories : IOrderRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public OrderRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addorders(orders orders)
        {
            try
            {
                cuaHangDbContext.orders.Add(orders);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<orders> GetAll()
        {
            var result = cuaHangDbContext.orders.Include(x => x.employees).Include(x => x.customers).ToList();
            return result;
        }

        public orders GetByID(Guid id)
        {
            var order = cuaHangDbContext.orders.FirstOrDefault(p => p.Id == id);
            return order;
        }

        public bool Removeorders(orders orders)
        {

            try
            {
                cuaHangDbContext.orders.Remove(orders);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateorders(orders orders)
        {

            try
            {
                cuaHangDbContext.orders.Update(orders);
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
