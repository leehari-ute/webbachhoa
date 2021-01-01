using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;
using PagedList;
using System.Web.Mvc.Html;

namespace BHX_2.Controllers
{
    public class ListProductsController : Controller
    {
        // GET: ListProducts
        LTWebEntities db = new LTWebEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadList(int? page,string KeyG ="")
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            if (KeyG == null)
            {
                return RedirectToAction("HomeBHX", "HomeBHX");
            }
            else
            {
                ViewBag.keyG = KeyG;
                string chuoi = "";
                chuoi += "<div class=\"milk_1\">";

                chuoi += "<ul>";

                chuoi += "<li>";
                chuoi += "<a> <img class=\"imgmilk_1\" src=\"/Content/Images/" + KeyG + "/1.png\" /></a>";
                chuoi += "</li>";

                chuoi += "<li>";
                chuoi += "<a> <img class=\"imgmilk_1\" src=\"/Content/Images/" + KeyG + "/2.png\" /></a>";
                chuoi += "</li>";

                chuoi += "<li>";
                chuoi += "<a> <img class=\"imgmilk_1\" src=\"/Content/Images/" + KeyG + "/3.png\" /></a>";
                chuoi += "</li>";

                chuoi += "</ul>";

                chuoi += "</div>";

                chuoi += "<div class=\"milk_2\">";
                chuoi += "<ul>";

                chuoi += "<h1>" + KeyG + "</h1>";
                var product = (from p in db.Products where p.ProductGroup == KeyG orderby p.ProductID descending select p).OrderBy(p => p.ProductID).ToList();
                
                for (int i = pageNumber*10-10 ; i < pageNumber*10 && i < product.Count; i++)
                {
                    chuoi += "<li>";
                    chuoi += "<img class=\"imgmilk_2\" src=\"/" + product[i].UrlImage + "\" />";
                    chuoi += "<h2>" + product[i].ProductName + "</h2>";
                    chuoi += "<h3>" + product[i].Price + " đồng" + "</h3>";                    
                    chuoi += "<h4 class=\"giohang\"> <a <i class=\"fas fa-shopping-cart\"></i>";
                    chuoi += "<a href=\"../Cart/AddtoCart/?ProductID=" + product[i].ProductID + "\">Mua </a>";
                    chuoi += "</a>";
                    chuoi += "</h4>";
                    chuoi += "</li>";            
                }
                chuoi += "</ul>";
                chuoi += "</div>";
                ViewBag.View = chuoi;
                return PartialView("_PartialViewLoadList", product.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult DetailProduct(int productID = 0)
        {
            if (productID == 0)
            {
                return View();
            }
            else
            {
                var detail = db.Products.Where(s => s.ProductID == productID)
                    .Select(s => new 
                    { s.ProductID,
                    s.ProductName,
                    s.Amount,
                    s.ProductGroup,
                    s.UrlImage,
                    s.Image,
                    s.Price,
                    s.Description}).ToList();
                string chuoi = "";

                chuoi += "<div class=\"milk_2\">";
                chuoi += "<img class=\"imgmilk_2\" src=\"/" + detail[0].UrlImage + "\" />";
                chuoi += "<h2>" + detail[0].ProductName + "</h2>";
                chuoi += "<h3>" + detail[0].Price + " đồng" + "</h3>";
                chuoi += "<h3>" + detail[0].Description + "</h3>";
                chuoi += "<h4 class=\"giohang\"> <a style=\"margin:center;\" <i class=\"fas fa-shopping-cart\"></i>";
                chuoi += "<a href=\"DetailProduct/?productID=" + detail[0].ProductID + "\">Mua </a>";
                chuoi += "</a>";
                chuoi += "</h4>";
                chuoi += "</div>";

                ViewBag.View = chuoi;
                return View();
            }
        }
        public ActionResult AddToBag()
        {          
            return View();
        }
    }
}