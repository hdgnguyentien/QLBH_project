using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IProductRepositories
    {
        public IEnumerable<products> GetAll();
        public products GetByID(Guid id);
        public bool Addproducts(products products);
        public bool Updateproducts(products products);
        public bool Removeproducts(products products);
    }
}
