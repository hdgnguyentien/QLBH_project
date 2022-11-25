using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IProductDetailRepositories
    {
        IEnumerable<productdetails> GetAll();
        productdetails GetByID(Guid id);
        public bool Addproductdetails(productdetails productdetails);
        public bool Updateproductdetails(productdetails productdetails);
        public bool Removeproductdetails(productdetails productdetails);
    }
}
