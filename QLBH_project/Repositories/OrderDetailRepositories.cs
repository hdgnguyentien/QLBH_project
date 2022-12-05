using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class OrderDetailRepositories : IOrderDetailRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public OrderDetailRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addorderdetails(orderdetails orderdetails)
        {
            try
            {
                cuaHangDbContext.orderdetails.Add(orderdetails);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<orderdetails> GetAll()
        {
            var result = cuaHangDbContext.orderdetails.Include(x => x.orders).Include(x => x.productdetails).ToList();
            return result;
        }

        public orderdetails GetByID(Guid id)
        {
            var orderdetail = cuaHangDbContext.orderdetails.FirstOrDefault(p => p.Id == id);
            return orderdetail;
        }

        public bool Removeorderdetails(orderdetails orderdetails)
        {
            try
            {
                cuaHangDbContext.orderdetails.Remove(orderdetails);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateorderdetails(orderdetails orderdetails)
        {
            try
            {
                cuaHangDbContext.orderdetails.Update(orderdetails);
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
