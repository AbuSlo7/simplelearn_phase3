using BussinessAccessLayer.IServices;
using BussinessAccessLayer.ServicesImplementation;
using Online_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Online_Portal.Controllers
{
    public class ProductController : Controller
    {
        [UserAuthenticationFilter]
        // GET: Product
        public ActionResult Index(Models.Filter filter)
        {
            ILaptopService Service = new LaptopService();
            var ds = Service.GetProducts();
            var Products = ds.Tables[0].AsEnumerable()
            .Select(dataRow => new Product
            {
                ProductId = dataRow.Field<int>("ProductId"),
                ProductName = dataRow.Field<string>("ProductName"),
                IsAvaliable = dataRow.Field<bool>("IsAvaliable"),
                BrandName = dataRow.Field<string>("BrandName"),
                CategoryName = dataRow.Field<string>("CategoryName"),
                CategoryId = dataRow.Field<int>("CategoryId"),
                Color = dataRow.Field<string>("Color"),
                Price =dataRow.Field<decimal>("Price"),
                MainImg= dataRow.Field<string>("Img")
            }).ToList();

            if (filter.CategoryId > 0)
                Products = Products.Where(p => p.CategoryId == filter.CategoryId).ToList();
            if(!string.IsNullOrEmpty(filter.Brand))
                Products = Products.Where(p => p.BrandName == filter.Brand).ToList();
            if (!string.IsNullOrEmpty(filter.Color))
                Products = Products.Where(p => p.Color == filter.Color).ToList();
            if (!string.IsNullOrEmpty(filter.Processor))
                Products = Products.Where(p => p.Processor == filter.Processor).ToList();


            var _ds = Service.GetCategories();
            var Categories = _ds.Tables[0].AsEnumerable()
                .Select(datarow => new {
                    Id = datarow.Field<int>("CategoryId"),
                    Name = datarow.Field<string>("CategoryName")
                });
            ViewBag.categories = Categories;
            return View(Products);
        }
        [UserAuthenticationFilter]
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            decimal price = 0;
            ILaptopService Service = new LaptopService();
            var ds = Service.GetProductyId(id);
            var product = new Product();
            product.ProductId = Convert.ToInt32(ds.Rows[0]["ProductId"]);
            product.ProductName = (ds.Rows[0]["ProductName"]).ToString();
            product.IsAvaliable = (bool)ds.Rows[0]["IsAvaliable"];
            product.BrandName = (ds.Rows[0]["BrandName"]).ToString();
            product.CategoryName = (ds.Rows[0]["CategoryName"]).ToString();
            product.MainImg = (ds.Rows[0]["Img"]).ToString();
            product.Color = (ds.Rows[0]["Color"]).ToString();
            product.Processor = (ds.Rows[0]["Processor"]).ToString();
            product.ScreenSize = (ds.Rows[0]["ScreenSize"]).ToString();
            product.Description = (ds.Rows[0]["Description"]).ToString();
            decimal.TryParse((ds.Rows[0]["Price"]).ToString(), out price);
            product.Price = price;
            return View(product);
        }
        [UserAuthenticationFilter]
        public ActionResult AddToCart(int id)
        {
            decimal price = 0;
            ILaptopService Service = new LaptopService();
            var ds = Service.GetProductyId(id);
            var product = new Product();
            product.ProductId = Convert.ToInt32(ds.Rows[0]["ProductId"]);
            product.ProductName = (ds.Rows[0]["ProductName"]).ToString();
            product.IsAvaliable = (bool)ds.Rows[0]["IsAvaliable"];
            product.BrandName = (ds.Rows[0]["BrandName"]).ToString();
            product.CategoryName = (ds.Rows[0]["CategoryName"]).ToString();
            product.Color = (ds.Rows[0]["Color"]).ToString();
            product.Processor = (ds.Rows[0]["Processor"]).ToString();
            product.ScreenSize = (ds.Rows[0]["ScreenSize"]).ToString();
            decimal.TryParse((ds.Rows[0]["Price"]).ToString(),out price);
            product.Price = price;
            if (Session["cart"] == null)
            {
                List<Product> cart = new List<Product>();
                cart.Add(product);
                Session["cart"] = cart;
            }
            else
            {
                List<Product> cart = (List<Product>)Session["cart"];
                cart.Add(product);
                Session["cart"] = cart;
            }
            return RedirectToAction("Cart");
        }
        [UserAuthenticationFilter]
        public ActionResult Cart()
        {
            List<Product> cart;
            if (Session["cart"] == null)
            {
                cart = new List<Product>();
                Session["cart"] = cart;
            }
            else
            {
                cart = (List<Product>)Session["cart"];
                Session["cart"] = cart;
            }
            return View(cart);
        }
        [UserAuthenticationFilter]
        public ActionResult Remove(int id)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        [UserAuthenticationFilter]
        private int isExist(int id)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == id)
                    return i;
            }
            return -1;
        }
    }
}