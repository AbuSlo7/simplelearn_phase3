using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IDBRepository
    {
        DataSet GetProducts();
        int submitOrder(int Productid, string OrderNumber, int CustomerId);
        int Login(string name, string Password);
        DataTable GetProductyId(int id);
        bool Register(string Fname, string Lname, string email, string password);
        string IsCustomerExists(string email);
        DataSet GetCategories();
        int AddProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available);
        bool EditProduct(decimal price, string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available,int productId);
        void AddProductImage(int prodId, string imageUrl);
    }
}
