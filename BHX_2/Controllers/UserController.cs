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
                    
                    string uname = Session["Username"].ToString();

                    var _db = db.Users.Where(s => s.Password == pass && s.Username == uname).ToList();
                    string sms = "";
                    if (_db.Count > 0)
                    {
                        string email = _db.FirstOrDefault().Email;
                        Random rd = new Random();
                        int code = rd.Next(100000, 999999);
                        CodeAndInfo newCode = new CodeAndInfo()
                        {
                            ID = 0, // bang 0 doi mat khau
                            newPass = newpass1,
                            code = code,
                            newBirth = DateTime.Now,
                        };
                        db.codeAndInfos.Add(newCode);
                        db.SaveChanges();
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
        public ActionResult ChangeInfo(string name, string birthday, string phone, string email, string address, string pass)
        {
            string uname = Session["Username"].ToString();          
            if (ModelState.IsValid)
            {
                var _db = db.Users.Where(s=>s.Username == uname).ToList();
                string checkPass = _db.FirstOrDefault().Password.Trim();
                if (checkPass == pass)
                {                  
                    string sms = "";
                    if (_db.Count > 0)
                    {
                        string oldemail = _db.FirstOrDefault().Email;
                        Random rd = new Random();
                        int code = rd.Next(100000, 999999);
                        if (name == "" || name == null)
                        {
                            name = _db.FirstOrDefault().HoTen.Trim();
                        }
                        if (birthday == "" || birthday == null)
                        {
                            birthday = _db.FirstOrDefault().ngaySinh.ToString("dd/MM/yyyy");
                        }
                        if (phone == "" || phone == null)
                        {
                            phone = _db.FirstOrDefault().Phone.ToString().Trim();
                        }
                        if (email == "" || email == null)
                        {
                            email = _db.FirstOrDefault().Email.Trim();
                        }
                        if (address == "" || address == null )
                        {
                            address = _db.FirstOrDefault().DiaChi.Trim();
                        }
                        CodeAndInfo newCode = new CodeAndInfo()
                        {
                            ID = 1, //bang 1 doi thong tin
                            code = code,
                            newBirth = DateTime.Parse(birthday),
                            newAdd = address,
                            newMail = email,
                            newPhone = phone,
                            newName = name,

                        };
                        db.codeAndInfos.Add(newCode);
                        db.SaveChanges();
                        sms += "Chào " + uname + ". Mã xác thực của bạn là: ";
                        MailMessage mail = new MailMessage("leehari1312@gmail.com", oldemail, sms, code.ToString());
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
                        return View();
                    }
                    return View("ConfirmCode");
                }
            }
            //ModelState.Clear();
            return View();
        }
        public ActionResult ConfirmCode(int code)
        {
            if (code == db.codeAndInfos.FirstOrDefault().code)
            {
                string uname = Session["Username"].ToString();
                var user = db.Users.Where(s => s.Username == uname).FirstOrDefault();
                if (db.codeAndInfos.FirstOrDefault().ID == 0)
                {
                    user.Password = db.codeAndInfos.FirstOrDefault().newPass.Trim();
                    db.SaveChanges();
                    var delCode = db.codeAndInfos.FirstOrDefault();
                    db.codeAndInfos.Remove(delCode);
                    db.SaveChanges();
                    return View("Personal");
                }
                else
                {
                    user.DiaChi = db.codeAndInfos.FirstOrDefault().newAdd.Trim();
                    user.HoTen = db.codeAndInfos.FirstOrDefault().newName.Trim();
                    user.ngaySinh = db.codeAndInfos.FirstOrDefault().newBirth;
                    user.Phone = int.Parse(db.codeAndInfos.FirstOrDefault().newPhone.Trim());
                    user.Email = db.codeAndInfos.FirstOrDefault().newMail.Trim();
                    db.SaveChanges();
                    var delCode = db.codeAndInfos.FirstOrDefault();
                    db.codeAndInfos.Remove(delCode);
                    db.SaveChanges();
                    return View("Personal");
                }
            }
            else
            {
                return View();
            }
        }
    }
}