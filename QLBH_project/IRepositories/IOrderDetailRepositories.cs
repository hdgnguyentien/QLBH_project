using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IOrderDetailRepositories
    {
        IEnumerable<orderdetails> GetAll();
        orderdetails GetByID(Guid id);
        public bool Addorderdetails(orderdetails orderdetails);
        public bool Updateorderdetails(orderdetails orderdetails);
        public bool Removeorderdetails(orderdetails orderdetails);
    }
}
