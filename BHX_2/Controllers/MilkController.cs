using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;

namespace BHX_2.Controllers
{
    public class MilkController : Controller
    {
        LTWebEntities db = new LTWebEntities();
        // GET: Milk
        public ActionResult Milk(string KeyG ="")
        {
            if (KeyG == null)
            {
                return RedirectToAction("HomeBHX", "HomeBHX");
            }
            else
            {
                string chuoi = "";
                var product = (from p in db.Products where p.ProductGroup == KeyG orderby p.ProductID descending select p).ToList();
                for (int i = 0; i < product.Count; i++)
                {
                    chuoi += "<li>";
                    chuoi += "<img class=\"imgmilk_2\" src=\"/" + product[i].UrlImage + "\" />";
                    chuoi += "<h2>" + product[i].ProductName + "</h2>";
                    chuoi += "<h3>" + product[i].Price + "</h3>";
                    chuoi += "<h4 class=\"giohang\"> <a> <i class=\"fas fa-shopping-cart\"> </i>  Mua </a> </h4>";
                    chuoi += "</li>";
                }
                ViewBag.View = chuoi;
                return View();
            }
        }
        public ActionResult Sua_tuoi_Dutch_Lady()
        {
            return View();
        }
        public ActionResult Sua_tuoi_Nuti_co_duong()
        {
            return View();
        }
    }
}