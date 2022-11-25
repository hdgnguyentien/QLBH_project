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
            return cuaHangDbContext.productdetails.ToList();
        }

        public productdetails GetByID(Guid id)
        {
            var prodetail = cuaHangDbContext.productdetails.FirstOrDefault(p => p.Id == id);
            return prodetail;
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
