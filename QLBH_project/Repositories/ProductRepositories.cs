using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public ProductRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addproducts(products products)
        {
            try
            {
                cuaHangDbContext.products.Add(products);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<products> GetAll()
        {
            return cuaHangDbContext.products.ToList();
        }

        public products GetByID(Guid id)
        {
            var product = cuaHangDbContext.products.FirstOrDefault(p=>p.Id == id);
            return product;
        }

        public bool Removeproducts(products products)
        {
            try
            {
                cuaHangDbContext.products.Remove(products);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateproducts(products products)
        {
            try
            {
                cuaHangDbContext.products.Update(products);
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
