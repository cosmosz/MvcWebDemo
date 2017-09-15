using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCShopping.Models;

namespace MVCShopping.Controllers
{
    public class HomeController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        //首页
        public ActionResult Index()
        {
            ViewBag.resource = Resources.Resource1.TEST;
            var data = db.ProductCategorys.ToList();
            return View(data);
        }

        //商品列表
        public ActionResult ProductList(int id)
        {
            var productCategory = db.ProductCategorys.Find(id);
            if(productCategory != null)
            {
                var data = productCategory.Products.ToList();
                if (data.Count > 0)
                {
                    return View(data);
                }
            }
            return HttpNotFound();
        }

        //商品明细
        public ActionResult ProductDetail(int id)
        {
            var data = db.Products.Find(id);
            return View(data);
        }
    }
}