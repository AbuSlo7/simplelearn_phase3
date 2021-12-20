using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.IServices
{
    public interface ILaptopService
    {
        DataSet GetProducts();
        DataSet GetCategories();
        int submitOrder(int Productid, string OrderNumber, int CustomerId);
        int Login(string name, string Password);
        bool Register(string Fname, string Lname, string email, string password);
        string IsCustomerExists(string email);
        DataTable GetProductyId(int id);
        int AddProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available);
        bool EditProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available,int productId);
        void AddProductImage(int prodId, string imageUrl);
    }
}
