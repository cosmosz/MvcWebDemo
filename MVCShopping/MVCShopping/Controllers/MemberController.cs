using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCShopping.Models;
using System.Web.Security;

namespace MVCShopping.Controllers
{
    public class MemberController : Controller
    {
        ShoppingContext db = new ShoppingContext();
        private string pwSalt = "AlrySql"; //密码哈希所需的Salt随机数值
        // 会员注册页面
        public ActionResult Register()
        {
            return View();
        }

        // 会员注册页面
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "RegisterOn,AuthCode")]Member member)
        {
            //检查会员是否存在
            var chk_member = db.Members.Where(p => p.Email == member.Email).FirstOrDefault();
            if(chk_member != null)
            {
                ModelState.AddModelError("Email","您输入的Email已经被注册过了");
            }
            if (ModelState.IsValid)
            {
                member.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + member.Password, "SHA1");
                member.RegisterOn = DateTime.Now;
                member.AuthCode = Guid.NewGuid().ToString();

                db.Members.Add(member);
                db.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }
        }

        // 显示会员登录页面
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //运行会员登录
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(string email,string password,string returnUrl)
        {
            if(VaildateUser(email,password))
            {
                FormsAuthentication.SetAuthCookie(email,false);
                if(string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            ModelState.AddModelError("","您输入的账号或密码错误");
            return View();
        }

        private bool VaildateUser(string email, string password)
        {
            var hash_pw = FormsAuthentication.HashPasswordForStoringInConfigFile(pwSalt + password,"SHA1");
            var member = db.Members.Where(p => p.Email == email && p.Password == hash_pw).FirstOrDefault();
            return (member != null);
        }

        //运行会员注销
        public ActionResult Logout()
        {
            //清除窗体验证的Cookies
            FormsAuthentication.SignOut();

            //清除所有曾经写入过的Session信息
            Session.Clear();

            return RedirectToAction("Index","Home");
        }

        public ActionResult CheckDup(string Email)
        {
            var member = db.Members.Where(p => p.Email == Email).FirstOrDefault();
            if(member != null)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}