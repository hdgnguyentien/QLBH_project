using System;
using System.Collections.Generic;

namespace QLBH_project.Models
{
    public class customers
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<cart> carts { get; set; }
        public List<orders> orders { get; set; }
    }
}
