using System;

namespace QLBH_project.Models
{
    public class orderdetails
    {
        public Guid Id { get; set; }
        public Guid ProductDetailID { get; set; }
        public Guid OrderId { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public productdetails productdetails { get; set; }
        public orders orders { get; set; }
    }
}
