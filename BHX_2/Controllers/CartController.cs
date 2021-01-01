using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;
using System.Collections;
using PagedList;

namespace BHX_2.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
         LTWebEntities  db = new LTWebEntities();
        public ActionResult Index()
        {
            List<Cart> giohang = Session["giohang"] as List<Cart>;
            return View(giohang);
        }

        // thêm sản phẩm vào giỏ hàng
        public RedirectToRouteResult AddtoCart(int ProductID)
        {
            if (Session["giohang"] == null)
            {               
                Session["giohang"] = new List<Cart>();
            }           
            List<Cart> giohang = Session["giohang"] as List<Cart>;
            if (giohang.FirstOrDefault(m => m.ID == ProductID) == null)
            {
                Product sp = db.Products.Find(ProductID);
                Cart newItem = new Cart();                
                newItem.ID = ProductID;
                newItem.IDCart = 0;
                newItem.IDuser = 0;
                newItem.ProductName = sp.ProductName;
                newItem.Amount = 1;
                newItem.Status = "Chưa xác nhận";
                //var listKM = db.Products.Find(ProductID).SUKIENs.ToList();
                //if (listKM.Count > 0)
                //{
                //    newItem.DonGia = Convert.ToDouble(sp.KhuyenMai);
                //}
                //else
                //{
                //    newItem.DonGia = Convert.ToDouble(sp.Gia);
                //}
                newItem.Price = Convert.ToDouble(sp.Price);
                giohang.Add(newItem);
            }
            else // sản phẩm đã tồn tại trong giỏ hàng
            {
                Cart Item = giohang.FirstOrDefault(m => m.ID == ProductID);
                Item.Amount++;
            }
            Session["giohang"] = giohang;

            return RedirectToAction("Index");
            
        }

        // cập nhập sản phẩm trong giỏ hàng
        public RedirectToRouteResult Update(int ProductID, string txtSoLuong)
        {
            List<Cart> giohang = Session["giohang"] as List<Cart>;
            Cart item = giohang.FirstOrDefault(m => m.ID == ProductID);
            if (item != null)
            {
                item.Amount = Convert.ToInt32(txtSoLuong);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }

        // xóa sản phẩm trong giỏ hàng
        public RedirectToRouteResult DelCartItem(int ProductID)
        {
            List<Cart> giohang = Session["giohang"] as List<Cart>;
            Cart item = giohang.FirstOrDefault(m => m.ID == ProductID);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }
        // oder sản phẩm từ giỏ hàng // gửi mail
        public ActionResult Order()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("SignIn", "Login");
            }

            int idcart;
            try
            {
                idcart = db.Carts.Select(s => s.IDCart).Max() + 1;
            }
            catch 
            {
                idcart = 1;
            }
            string username = Session["Username"].ToString().Trim();
            int iduser = db.Users.Where(s => s.Username.Trim() == username).Select(s => s.idUser).FirstOrDefault();
            List<Cart> giohang = Session["giohang"] as List<Cart>;
            foreach(Cart oItem in giohang)
            {
                oItem.IDCart = idcart;
                oItem.IDuser = iduser;
                db.Carts.Add(oItem);
                db.SaveChanges();
            }
            var gia = db.Carts.Where(s => s.IDCart == idcart).Sum(s => s.Price);          
            var stt = db.Carts.Where(s => s.IDCart == idcart).Select(s => s.Status).FirstOrDefault();
            var newCart = new ListCart()
            {
                IDCart = idcart,
                TotalPrice = gia,
                IDuser = iduser,
                Status = stt.Trim(),                         
            };
            db.listCarts.Add(newCart);
            db.SaveChanges();
            return RedirectToAction("ListBill");

            #region ai biet gi
            //string Sms = "<htmt><body><table border='1'><caption> Thông tin đặt hàng</caption><tr><th> STT </th><th> Tên Sản Phẩm </th><th> Số lượng </th> <th> Đơn giá </th> <th> Thành tiền</th></tr>";
            //int i = 0;
            //double tongtien = 0;
            //foreach (Cart item in giohang)
            //{
            //    i++;
            //    Sms += "<tr>";
            //    Sms += "<td>" + i.ToString() + "</td>";
            //    Sms += "<td>" + item.ProductName + "</td>";
            //    Sms += "<td>" + item.Status + "</td>";
            //    Sms += "<td>" + item.Price + "</td>";
            //    Sms += "<td>" + string.Format("{0:#,###}", item.Amount * item.Price) + "</td>";
            //    Sms += "</tr>";
            //    tongtien += item.Amount * item.Price;

            //}
            //if (Email == null || Email == "")
            //{
            //    ViewBag.Er = "vui lòng nhập Email đặt hàng";

            //}
            //else
            //{

            //    Sms += "<tr><th colspan='5'> Tổng cộng: " + string.Format("{0:#,### vnđ}", tongtien) + "</th> </tr> </table>";
            //    MailMessage mail = new MailMessage("ducphung51194@gmail.com", Email, "Thông tin đơn hàng", Sms);
            //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //    client.EnableSsl = true;
            //    client.Credentials = new NetworkCredential("ducphung51194@gmail.com", "vinhduc147a");
            //    mail.IsBodyHtml = true;
            //    client.Send(mail);
            //}
            #endregion
        }
        public ActionResult ListBill()
        {
            string username = Session["Username"].ToString().Trim();              
            int iduser = db.Users.Where(s => s.Username.Trim() == username).Select(s => s.idUser).FirstOrDefault();
            var bill = (from p in db.Carts where p.IDuser == iduser orderby p.IDCart descending select p).ToList();
            var idcart = bill.Select(s => s.IDCart).Distinct().ToList();
            ViewBag.idCart = idcart;
            return View(bill);    
        }
        public PartialViewResult GetPaging(int? page)
        {
            int pageSize = 10;
            IQueryable<ListCart> prod = (from c in db.listCarts select c)
                    .OrderBy(x => x.IDCart);
            //if (Session["Lever"].ToString() == "3")
            //{
            //    string name = Session["Username"].ToString();
            //    int iduser = db.Users.Where(s => s.Username == name).Select(s=>s.idUser).FirstOrDefault();
            //    prod = (from c in db.listCarts where c.IDuser == iduser select c)
            //    .OrderBy(x => x.IDCart);
            //}             
            int pageNumber = (page ?? 1);
            return PartialView("_PartialViewListBill", prod.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ListBillAdmin()
        {
            return View();
        }
        public ActionResult Confirm(int id)
        {
            var confirm = db.listCarts.First(s => s.IDCart == id);
            confirm.IDuser = db.listCarts.Where(s => s.IDCart == id).Select(s => s.IDuser).FirstOrDefault();
            confirm.Status = "Đã xác nhận";
            confirm.TotalPrice = db.listCarts.Where(s => s.IDCart == id).Select(s => s.TotalPrice).FirstOrDefault();
            db.SaveChanges();

            var reset = db.Carts.Where(s => s.IDCart == id).ToList();
            foreach (var item in reset)
            {
                item.Status = "Đã xác nhận";
                db.SaveChanges();
            }

            return RedirectToAction("GetPaging");       
        }
        public ActionResult Ship(int id)
        {
            var ship = db.listCarts.First(s => s.IDCart == id);
            ship.IDuser = db.listCarts.Where(s => s.IDCart == id).Select(s => s.IDuser).FirstOrDefault();
            ship.Status = "Đang giao hàng";
            ship.TotalPrice = db.listCarts.Where(s => s.IDCart == id).Select(s => s.TotalPrice).FirstOrDefault();
            db.SaveChanges();

            var reset = db.Carts.Where(s => s.IDCart == id).ToList();
            foreach (var item in reset)
            {
                item.Status = "Đang giao hàng";
                db.SaveChanges();
            }
            return RedirectToAction("GetPaging");
        }
        public ActionResult Cancel(int id)
        {
            var ship = db.listCarts.First(s => s.IDCart == id);
            ship.IDuser = db.listCarts.Where(s => s.IDCart == id).Select(s => s.IDuser).FirstOrDefault();
            ship.Status = "Đã hủy";
            ship.TotalPrice = db.listCarts.Where(s => s.IDCart == id).Select(s => s.TotalPrice).FirstOrDefault();
            db.SaveChanges();

            var reset = db.Carts.Where(s => s.IDCart == id).ToList();
            foreach (var item in reset)
            {
                item.Status = "Đã hủy";
                db.SaveChanges();
            }
            return RedirectToAction("GetPaging");
        }
        public ActionResult Received(int id)
        {
            var ship = db.listCarts.First(s => s.IDCart == id);
            ship.IDuser = db.listCarts.Where(s => s.IDCart == id).Select(s => s.IDuser).FirstOrDefault();
            ship.Status = "Đã nhận hàng";
            ship.TotalPrice = db.listCarts.Where(s => s.IDCart == id).Select(s => s.TotalPrice).FirstOrDefault();
            db.SaveChanges();

            var reset = db.Carts.Where(s => s.IDCart == id).ToList();
            foreach (var item in reset)
            {
                item.Status = "Đã nhận hàng";
                db.SaveChanges();
            }
            return RedirectToAction("GetPaging");
        }
    }
}
