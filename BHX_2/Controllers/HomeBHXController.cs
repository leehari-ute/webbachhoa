using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;

namespace BHX_2.Controllers
{
    public class HomeBHXController : Controller
    {

        LTWebEntities db = new LTWebEntities();
        // GET: HomeBHX
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Basket()
        {
            return View();
        }

        public ActionResult HomeBHX(string searchName ="")
        {
            string chuoi = "";

            var proG = db.Products.Select(s => s.ProductGroup).Distinct().ToList();
            foreach(var G in proG)
            {
                chuoi += "<div class=\"rau_1\">";
                chuoi += "<h5> <i class=\"fas fa-hand-point-right\"> </i> </h5>";
                var product = (from p in db.Products where p.ProductGroup == G orderby p.ProductID descending select p).ToList();
                if (!String.IsNullOrEmpty(searchName))
                {
                    product = product.Where(s => s.ProductName.Contains(searchName)).Take(5).ToList();
                }
                else
                {
                    product = product.Take(5).ToList(); 
                }
                chuoi += "<h1>" + G + "</h1>";
                chuoi += "</div>";
                for (int i = 0; i < product.Count; i++)
                {
                    
                    chuoi += "<li>";
                    chuoi += "<div class=\"banchay\">";
                    chuoi += "<div class=\"banchay1\"><h6>Bán chạy</h6></div>";
                    chuoi += "</div>";
                    chuoi += "<img class=\"imgrau\" src=\"/" + product[i].UrlImage + "\" />";
                    chuoi += "<h3>" + product[i].ProductName + "</h3>";
                    chuoi += "<h2>" + product[i].Price + " đồng"+"</h2>";
                    chuoi += "<h4 class=\"giohang\"> <a <i class=\"fas fa-shopping-cart\"></i>";
                    chuoi += "<a href=\"../Cart/AddtoCart/?ProductID=" + product[i].ProductID + "\">Mua </a>";
                    chuoi += "</a>";
                    chuoi += "</h4>";
                    chuoi += "</li>";
                    
                    if (i == 4)
                    {
                        break;
                    }
                }
                chuoi += "</br>";
                chuoi += "</br>";
                
            }

            ViewBag.View = chuoi;
            return View();
        }
        public ActionResult Search()
        {
           // var pro = (from p in db.Products where  orderby productId descending select p).ToList();
           // từ từ :>

            return View();
        }
        public ActionResult ThongTin()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult HoiDap()
        {
            return View();
        }
        public ActionResult KhachHang()
        {
            return View();
        }

    }
}