using Microsoft.EntityFrameworkCore;
using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class ProductDetailRepositories : IProductDetailRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public ProductDetailRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addproductdetails(productdetails productdetails)
        {
            try
            {
                cuaHangDbContext.productdetails.Add(productdetails);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<productdetails> GetAll()
        {
            var productdetails = cuaHangDbContext.productdetails.Include(p => p.categories).Include(p => p.products).ToList();
            return productdetails;
        }

        public productdetails GetByID(Guid id)
        {
            var productdetails = cuaHangDbContext.productdetails.Include(p => p.categories)
                                                                .Include(p => p.products)
                                                                .FirstOrDefault(x=>x.Id == id);
            return productdetails;
        }

        public bool Removeproductdetails(productdetails productdetails)
        {
            try
            {
                cuaHangDbContext.productdetails.Remove(productdetails);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateproductdetails(productdetails productdetails)
        {
            try
            {
                cuaHangDbContext.productdetails.Update(productdetails);
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
