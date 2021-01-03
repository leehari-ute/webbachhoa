using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHX_2.Models;
using System.Data;
using System.IO;
using System.Configuration;
using OfficeOpenXml;

namespace BHX_2.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        LTWebEntities db =  new LTWebEntities();


        public ActionResult Index()
        {
            var collection = db.Products.ToList();
            return View(collection);
        }
        public void DownloadExcel()
        {
            var collection = db.Products.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "ProductID";
            Sheet.Cells["B1"].Value = "ProductName";
            Sheet.Cells["C1"].Value = "ProductGroup";
            Sheet.Cells["D1"].Value = "Price";
            Sheet.Cells["E1"].Value = "Amount";
            Sheet.Cells["F1"].Value = "Description";
            Sheet.Cells["G1"].Value = "Image";
            Sheet.Cells["H1"].Value = "UrlIamge";
            int row = 2;
            foreach (var item in collection)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.ProductID;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.ProductName;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.ProductGroup;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Price;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Amount;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.Description;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Image;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.UrlImage;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }
    }
} 
    
