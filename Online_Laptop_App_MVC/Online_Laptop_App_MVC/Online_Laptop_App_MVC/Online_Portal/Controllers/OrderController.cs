using BussinessAccessLayer.IServices;
using BussinessAccessLayer.ServicesImplementation;
using Online_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Portal.Controllers
{
    public class OrderController : Controller
    {
        [UserAuthenticationFilter]
        // GET: Order
        public ActionResult Index()
        {
            // save order to database 
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
            var orderNumber = (Session["CustomerId"].ToString()) + "_" + new Random().Next(10000,100000);
            ViewBag.orderNumber = orderNumber;
            ILaptopService Service = new LaptopService();
            int result = 0;
            foreach (Product item in cart) 
            {
                result = Service.submitOrder(item.ProductId, orderNumber, Convert.ToInt32(Session["CustomerId"].ToString()));
                if (result == -1)
                {
                    Session["cart"] = null;
                    RedirectToAction("Error");
                }
            }
            
            Session["cart"] = null;
            return View(cart);
        }
    }
}