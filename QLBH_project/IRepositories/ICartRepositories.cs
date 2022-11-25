using QLBH_project.Models;
using System;
using System.Collections.Generic;

namespace QLBH_project.IRepositories
{
    public interface ICartRepositories
    {
        IEnumerable<cart> GetAll();
        cart GetByID(Guid id);
        public bool AddCart(cart cart);
        public bool UpdateCart(cart cart);
        public bool RemoveCart(cart cart);
    }
}
