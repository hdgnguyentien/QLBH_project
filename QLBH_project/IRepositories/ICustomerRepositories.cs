using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface ICustomerRepositories
    {
        IEnumerable<customers> GetAll();
        customers GetByID(Guid id);
        public bool Addcustomers(customers customers);
        public bool Updatecustomers(customers customers);
        public bool Removecustomers(customers customers);
    }
}
