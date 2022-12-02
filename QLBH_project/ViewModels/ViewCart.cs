using QLBH_project.Models;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.ViewModels
{
    public class ViewCart
    {
        public List<CartItem> cartItems { get; set; }
        public decimal grandtotal { get; set; }
    }
}
