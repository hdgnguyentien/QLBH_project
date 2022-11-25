using System;

namespace QLBH_project.Models
{
    public class cart
    {
        public Guid  Id { get; set; }
        public Guid CustomerID { get; set; }
        public Guid ProductDetailID { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public customers customers { get; set; }
        public productdetails productdetails { get; set; }
    }
}
