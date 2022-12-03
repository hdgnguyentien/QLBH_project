using Microsoft.AspNetCore.Mvc;
using QLBH_project.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace QLBH_project.Helpers
{
    public class BagCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            BagCartViewModel bagCartView;

            if (cart==null||cart.Count==0)
            {
                bagCartView = null;
            }
            else
            {
                bagCartView = new BagCartViewModel()
                {
                    numberOfItems = cart.Sum(x=>x.Quatity)
                };
            }
            return View(bagCartView);
        }
    }
}
