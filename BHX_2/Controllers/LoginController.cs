using BHX_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net.Mail;
using System.Net;

namespace BHX_2.Controllers
{
    public class LoginController : Controller
    {
        private LTWebEntities db = new LTWebEntities();
        // GET: Login
        public ActionResult Index(FormCollection Fields)
        {
            string Username = Fields["Username"];
            string Password = Fields["Password"];
            string Lever = Fields["Lever"];
            return View();
        }
        public ActionResult ForgotPassword(string Uname, string email)
        {
            if (ModelState.IsValid)
            {
                var _db = db.Users.Where(s => s.Username.ToString().Trim().Equals(Uname) && s.Email.ToString().Trim().Equals(email)).ToList();
                string sms = "";
                if (_db.Count > 0)
                {
                    string username = _db.FirstOrDefault().Username;
                    string pass = _db.FirstOrDefault().Password;
                    sms += "Chào bạn. Thông tin mật khẩu của bạn là:" + " " + username;
                    MailMessage mail = new MailMessage("leehari1312@gmail.com", email, sms, pass);
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("leehari1312@gmail.com", "13122802");
                    mail.IsBodyHtml = true;
                    client.Send(mail);
                    ViewBag.suc = "Get Success! Check your mail";
                }
                else if (_db.Count == 0)
                {
                    // ModelState.AddModelError("Invalid", "Username or password not exits");
                    return View("ForgotPassWord");
                }
            }
            ModelState.Clear();
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(FormCollection fields)
        {
            if (fields["Password"] != fields["PasswordAgain"])
                return View();
            using (LTWebEntities _db = new LTWebEntities())
            {
                User a = new User();
                a.Username = fields["Username"];
                a.Email = fields["gmail"];
                a.Password = fields["Password"];
                a.Phone = int.Parse(fields["numberPhone"]);
                a.Lever = 3;
                a.HoTen = "";
                a.DiaChi = "";
                a.ngaySinh = DateTime.Now;
                _db.Users.Add(a);
                _db.SaveChanges();
            }
                return View();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string Username, string Password)
        {
            User a = new User();
            using (LTWebEntities _db = new LTWebEntities())
            {
                a = _db.Users.Where(x => x.Username == Username).FirstOrDefault();
            }

            if (a == null)  return View();

            if(a.Password == Password && a.Lever == 1)
            {
                Session["Username"] = Username;
                Session["Lever"] = "1";
                return RedirectToAction("Index", "Admin");   
            }
            Session["Username"] = Username;

            if (Session["giohang"] != null)
            {
                return RedirectToAction("Index","Cart");
            }
            Session["Username"] = Username;
            Session["Lever"] = "3";
            return RedirectToAction("HomeBHX", "HomeBHX");

        }
        // : Get Log out
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("HomeBHX", "HomeBHX");
        }
        public PartialViewResult GetPaging(int? page)
        {
            int pageSize = 3;
            IQueryable<Product> prod = (from pro in db.Products select pro)
                .OrderBy(x => x.ProductID);
            int pageNumber = (page ?? 1);
            return PartialView("_PartialViewProduct", prod.ToPagedList(pageNumber, pageSize));
        }
    }
}