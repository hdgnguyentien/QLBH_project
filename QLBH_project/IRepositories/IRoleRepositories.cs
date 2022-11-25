using QLBH_project.Models;
using System.Collections.Generic;
using System;

namespace QLBH_project.IRepositories
{
    public interface IRoleRepositories
    {
        IEnumerable<roles> GetAll();
        roles GetByID(Guid id);
        public bool Addroles(roles roles);
        public bool Updateroles(roles roles);
        public bool Removeroles(roles roles);
    }
}
