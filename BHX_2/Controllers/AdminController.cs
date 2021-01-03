using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Where(s => s.idUser == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Where(s => s.idUser == id).FirstOrDefault();
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("UserIndex", "Admin");
        }
        public ActionResult Details(int id)
        {
            User user = db.Users.Where(s => s.idUser == id).FirstOrDefault();
            return View(user);
        }
    }
}