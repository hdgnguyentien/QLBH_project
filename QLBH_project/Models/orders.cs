using System;
using System.Collections.Generic;

namespace QLBH_project.Models
{
    public class orders
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DateCreate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Status { get; set; }
        public employees employees { get; set; }
        public customers customers { get; set; }
        public List<orderdetails> orderdetails { get; set; }
    }
}
