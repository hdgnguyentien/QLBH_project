using System;
using System.Collections.Generic;

namespace QLBH_project.Models
{
    public class roles
    {
        public Guid Id { get; set; }
        public string Rolename { get; set; }
        public List<employees> employees { get; set; }
    }
}
