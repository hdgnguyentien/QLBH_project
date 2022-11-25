using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface ICategoriesRepositories
    {
        IEnumerable<categories> GetAll();
        categories GetByID(Guid id);
        public bool Addcategories(categories categories);
        public bool Updatecategories(categories categories);
        public bool Removecategories(categories categories);
    }
}
