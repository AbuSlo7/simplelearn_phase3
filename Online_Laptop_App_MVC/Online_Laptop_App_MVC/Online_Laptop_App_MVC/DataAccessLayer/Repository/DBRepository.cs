using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class DBRepository : IDBRepository
    {

            public DataSet GetProducts()
            {
                return DataAccessHelper.GetProducts();
            }

        public int Login(string name, string Password)
        {
            return DataAccessHelper.Login(name, Password);
        }

        public bool Register(string Fname, string Lname, string email, string password)
        {
            return DataAccessHelper.Regester(Fname,Lname,email,password);
        }

        public string IsCustomerExists(string email)
        {
            return DataAccessHelper.IsCustomerExists(email);
        }

        public DataSet GetCategories()
        {
            return DataAccessHelper.GetCategories();
        }

        public int submitOrder(int Productid, string OrderNumber, int CustomerId)
        {
            return DataAccessHelper.submitOrder(Productid, OrderNumber, CustomerId);
        }
        public DataTable GetProductyId(int id)
        {
            return DataAccessHelper.GetProductyId(id);
        }

        public int AddProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available)
        {
            return DataAccessHelper.AddProduct(price,productName, color, description, screenSize, categoryId, brandName, processor, available);
        }

        public bool EditProduct(decimal price, string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor, bool available,int productId)
        {
            return DataAccessHelper.EditProduct(price,productName, color, description, screenSize, categoryId, brandName, processor, available,productId);
        }

        public void AddProductImage(int prodId, string imageUrl)
        {
            DataAccessHelper.AddProductImage(prodId, imageUrl);
        }
    }
}
