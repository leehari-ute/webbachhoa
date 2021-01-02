using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;

namespace BHX_2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        LTWebEntities db = new LTWebEntities();
        public int confirmCode;
        public string newpass;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Personal()
        {
            string uname = Session["Username"].ToString();
            var model = db.Users.Where(s => s.Username == uname).FirstOrDefault();
            return View(model);
        }
        public ActionResult ChangePass(string pass, string newpass1, string newpass2)
        {
            if (ModelState.IsValid)
            {
                if (newpass1 == newpass2)
                {
                    newpass = newpass1;
                    string uname = Session["Username"].ToString();

                    var _db = db.Users.Where(s => s.Password == pass && s.Username == uname).ToList();
                    string sms = "";
                    if (_db.Count > 0)
                    {
                        string email = _db.FirstOrDefault().Email;
                        Random rd = new Random();
                        int code = rd.Next(100000, 999999);
                        confirmCode = code;
                        sms += "Chào " + uname + ". Mã xác thực của bạn là: ";
                        MailMessage mail = new MailMessage("leehari1312@gmail.com", email, sms, code.ToString());
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
                        return View("ChangePass");
                    }
                    return View("ConfirmCode");
                }   
            }
            //ModelState.Clear();
            return View();
        }
        public ActionResult ConfirmCode(string code)
        {
            if (code == confirmCode.ToString())
            {
                var user = db.Users.Where(s => s.Username == Session["Username"].ToString()).FirstOrDefault();
                user.Password = newpass;
                db.SaveChanges();
                return View("Index");
            }
            else
            {
                return View();
            }
        }
    }
}