using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models; 

namespace BHX_2.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        LTWebEntities db = new LTWebEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserIndex()
        {
            List<User> user = db.Users.Where(s => s.Lever == 3).ToList();           
            return View(user);
        }
    }
}