using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCShopping.Models;

namespace MVCShopping.Controllers
{
    public class CartController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        List<Cart> Carts
        {
            get
            {
                if(Session["Carts"] == null)
                {
                    Session["Carts"] = new List<Cart>();
                }
                return Session["Carts"] as List<Cart>;
             }
            set { Session["Carts"] = value; }
        }

        // 添加产品项目到购物车，如果没有传入Amount参数则默认购买数量为1

        public ActionResult AddToCart(int ProductId,int Amount = 1)
        {
            var product = db.Products.Find(ProductId);
            if(product == null)
            {
                return HttpNotFound();
            }
            var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if(existingCart != null)
            {
                existingCart.Amount += 1;
            }
            else
            {
                this.Carts.Add(new Cart() { Product = product,Amount = Amount });
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Created);
        }

        //显示当前的购物车项目
        public ActionResult Index()
        {
            return View(this.Carts);
        }

        //移除购物车项目
        [HttpPost]
        public ActionResult Remove(int ProductId)
        {
            var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == ProductId);
            if(existingCart != null)
            {
                this.Carts.Remove(existingCart);
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        //更新购物车中特定项目的购买数量
        [HttpPost]
        public ActionResult UpdateAmount(List<Cart> Carts)
        {
            foreach (var item in Carts)
            {
                var existingCart = this.Carts.FirstOrDefault(p => p.Product.Id == item.Product.Id);
                if(existingCart != null)
                {
                    existingCart.Amount = item.Amount;
                }
            }
            return RedirectToAction("Index","Cart");
        }
    }
}