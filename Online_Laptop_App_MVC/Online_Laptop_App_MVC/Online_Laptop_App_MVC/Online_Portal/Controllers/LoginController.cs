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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new UserLogin());
        }
        public ActionResult ValidateUser(UserLogin userLogin)
        {
            if(userLogin.Email=="admin@gmail.com" && userLogin.Password == "adminadmin123")
            {
                Session["admin"] = true;
                return RedirectToAction("Products", "Admin");
            }
            ILaptopService Service = new LaptopService();
            var CustomerId = Service.Login(userLogin.Email, userLogin.Password);
            if (CustomerId >0)
            {
                Session["UserID"] = Guid.NewGuid();
                Session["CustomerId"] = CustomerId;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("", "Invalid credentails");
                return View("Index");
            }
        }
        [Route("user/register")]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [Route("user/register")]
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            //check either the email exists
            ILaptopService Service = new LaptopService();
           string email= Service.IsCustomerExists(customer.Email);
            if (!string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email already exists");
                return View();
            }

            bool result= Service.Register(customer.Fname, customer.Lname, customer.Email, customer.Password);
            if (!result)
            {
                ModelState.AddModelError("", "Registration failed");
                return View();
            }
            ViewBag.success = "Successfully registered";
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["CustomerId"] = null;
            Session["admin"] = null;
            return RedirectToAction("Index");
        }
    }
}