using QLBH_project.IRepositories;
using QLBH_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Repositories
{
    public class RoleRepositories : IRoleRepositories
    {
        CuaHangDbContext cuaHangDbContext;
        public RoleRepositories(CuaHangDbContext cuaHangDbContext)
        {
            this.cuaHangDbContext = cuaHangDbContext;
        }

        public bool Addroles(roles roles)
        {
            try
            {
                cuaHangDbContext.roles.Add(roles);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<roles> GetAll()
        {
            return cuaHangDbContext.roles.ToList();
        }

        public roles GetByID(Guid id)
        {
            var role = cuaHangDbContext.roles.FirstOrDefault(p => p.Id == id);
            return role;
        }

        public bool Removeroles(roles roles)
        {
            try
            {
                cuaHangDbContext.roles.Remove(roles);
                cuaHangDbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Updateroles(roles roles)
        {
            try
            {
                cuaHangDbContext.roles.Update(roles);
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
