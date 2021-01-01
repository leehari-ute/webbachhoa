using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;
using PagedList;

namespace BHX_2.Controllers
{
    public class ProductsController : Controller
    {
        private LTWebEntities db = new LTWebEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        
        public PartialViewResult GetPaging(int? page)
        {
            int pageSize = 10;          
            IQueryable<Product> prod = (from pro in db.Products select pro)
                .OrderBy(x => x.ProductID);
            int pageNumber = (page ?? 1);
            return PartialView("_PartialViewProduct", prod.ToPagedList(pageNumber, pageSize));
        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,ProductGroup,Price,Status,Description")] Product product, HttpPostedFileBase image)
        {
            if(image != null && image.ContentLength >0)
            {
                product.Image = new byte[image.ContentLength];
                image.InputStream.Read(product.Image, 0, image.ContentLength);
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlImage = Server.MapPath("~/Image/" + fileName);
                image.SaveAs(urlImage);

                product.UrlImage = "Image/" + fileName;
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,ProductGroup,Price,Status,Description")] Product product, HttpPostedFileBase image)
        {
            
            if (ModelState.IsValid)
            {
               // Product modifyProduct = db.Products.Find(product.ProductID);
                if ( product!= null)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        product.Image = new byte[image.ContentLength];
                        image.InputStream.Read(product.Image, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlImage = Server.MapPath("~/Image/" + fileName);
                        image.SaveAs(urlImage);
                        product.UrlImage = "Image/" + fileName;
                    }
                } 
                //db.Entry(modifyProduct).State = EntityState.Modified;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetPaging");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("GetPaging");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetOrders(string stat)
        {
            string uname = (string)Session["Username"];
            int id = db.Users.Where(s => s.Username == uname).Select(s=>s.idUser).FirstOrDefault();
            var model = db.listCarts.Select(s=>s).OrderBy(s=>s.IDCart).ToList();
            switch (stat)
            {
                case "Chuaxacnhan":
                    model = model.Where(x => x.Status == "Chưa xác nhận").ToList();
                    break;
                case "Daxacnhan":
                    model = model.Where(x => x.Status == "Đã xác nhận").ToList();
                    break;
                case "Danhanhang":
                    model = model.Where(x => x.Status == "Đã nhận hàng").ToList();
                    break;
                case "Danggiao":
                    model = model.Where(x => x.Status == "Đang giao hàng").ToList();
                    break;
                case "Dahuy":
                    model = model.Where(x => x.Status == "Đã hủy").ToList();
                    break;
                case "Tatca":
                    break;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
