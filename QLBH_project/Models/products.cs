using System;
using System.Collections.Generic;

namespace QLBH_project.Models
{
    public class products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public List<productdetails>  productdetails { get; set; }
    }
}
