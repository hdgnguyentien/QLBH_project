using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class CategoriesRepositories : ICategoriesRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public CategoriesRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addcategories(categories categories)
        {
            try
            {
                cuaHangDbContext.categories.Add(categories);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<categories> GetAll()
        {
            return cuaHangDbContext.categories.ToList();
        }

        public categories GetByID(Guid id)
        {
           var categories = cuaHangDbContext.categories.FirstOrDefault(p=>p.Id == id);
            return categories;
        }

        public bool Removecategories(categories categories)
        {
            try
            {
                cuaHangDbContext.categories.Remove(categories);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updatecategories(categories categories)
        {
             try
            {
                cuaHangDbContext.categories.Update(categories);
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
