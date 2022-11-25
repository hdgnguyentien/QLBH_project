using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IOrderRepositories
    {
        IEnumerable<orders> GetAll();
        orders GetByID(Guid id);
        public bool Addorders(orders orders);
        public bool Updateorders(orders orders);
        public bool Removeorders(orders orders);
    }
}
