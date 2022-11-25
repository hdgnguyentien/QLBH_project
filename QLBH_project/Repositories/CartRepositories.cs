using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class CartRepositories : ICartRepositories
    {
        public CuaHangDbContext cuaHangDbContext;
        public CartRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool AddCart(cart cart)
        {
            try
            {
                cuaHangDbContext.carts.Add(cart);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {   

                return false;
            }
        }

        public IEnumerable<cart> GetAll()
        {
            return cuaHangDbContext.carts.ToList();
        }

        public cart GetByID(Guid id)
        {
            var cart = cuaHangDbContext.carts.FirstOrDefault(p=>p.Id == id);
            return cart;
        }

        public bool RemoveCart(cart cart)
        {
            try
            {
                cuaHangDbContext.carts.Remove(cart);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool UpdateCart(cart cart)
        {
            try
            {
                cuaHangDbContext.carts.Update(cart);
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
