using BussinessAccessLayer.IServices;
using BussinessAccessLayer.ServicesImplementation;
using Online_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Portal.Controllers
{
    [AdminAuthenticationFilter]
    public class AdminController : Controller
    {
        public ActionResult Products()
        {
            ILaptopService Service = new LaptopService();
            var ds = Service.GetProducts();
            var Products = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Product
            {
                ProductId = dataRow.Field<int>("ProductId"),
                ProductName = dataRow.Field<string>("ProductName"),
                Price =dataRow.Field<decimal>("Price"),
                IsAvaliable = dataRow.Field<bool>("IsAvaliable"),
                BrandName = dataRow.Field<string>("BrandName"),
                CategoryName = dataRow.Field<string>("CategoryName"),
                MainImg= dataRow.Field<string>("Img")
            }).ToList();
            return View(Products);
        }
        public ActionResult AddProduct()
        {
            ILaptopService Service = new LaptopService();
            var ds = Service.GetCategories();
            var Categories = ds.Tables[0].AsEnumerable()
                .Select(datarow => new {
                    Id=datarow.Field<int>("CategoryId"),
                    Name=datarow.Field<string>("CategoryName")
                });
            ViewBag.categories = Categories;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product,HttpPostedFileBase[] images)
        {
            ILaptopService Service = new LaptopService();
            int prodId= Service.AddProduct(product.Price,product.ProductName,product.Color,product.Description,product.ScreenSize,product.CategoryId,product.BrandName,product.Processor,product.IsAvaliable);
            foreach(var item in images)
            {
                if (item != null)
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetFileName(item.FileName);
                    string outputFile = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/ProductImages/") + filename);
                    item.SaveAs(outputFile);
                    string imageUrl = $"~/ProductImages/{filename}";
                    Service.AddProductImage(prodId,imageUrl);
                }
            }
            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int prodId)
        {
            decimal price = 0;
            ILaptopService Service = new LaptopService();
            var ds = Service.GetProductyId(prodId);
            var product = new Product();
            product.ProductId = Convert.ToInt32(ds.Rows[0]["ProductId"]);
            product.ProductName = (ds.Rows[0]["ProductName"]).ToString();
            product.IsAvaliable = (bool)ds.Rows[0]["IsAvaliable"];
            product.BrandName = (ds.Rows[0]["BrandName"]).ToString();
            product.CategoryId = Convert.ToInt32(ds.Rows[0]["CategoryId"]);
            product.Color= (ds.Rows[0]["Color"]).ToString();
            product.ScreenSize= (ds.Rows[0]["ScreenSize"]).ToString();
            product.Description= (ds.Rows[0]["Description"]).ToString();
            product.Processor= (ds.Rows[0]["Processor"]).ToString();
            decimal.TryParse((ds.Rows[0]["Price"]).ToString(), out price);
            product.Price = price;

            var _ds = Service.GetCategories();
            var Categories = _ds.Tables[0].AsEnumerable()
                .Select(datarow => new {
                    Id = datarow.Field<int>("CategoryId"),
                    Name = datarow.Field<string>("CategoryName")
                });
            ViewBag.categories = Categories;
            return View(product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            ILaptopService Service = new LaptopService();
            Service.EditProduct(product.Price, product.ProductName, product.Color, product.Description, product.ScreenSize, product.CategoryId, product.BrandName, product.Processor, product.IsAvaliable,product.ProductId);
            return RedirectToAction("Products");
        }
    }
}