using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}