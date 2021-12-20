using BussinessAccessLayer.IServices;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessAccessLayer.ServicesImplementation
{
    public class LaptopService: ILaptopService
    {
        IDBRepository _dBRepository;

        public DataSet GetProducts()
        {
            _dBRepository = new DBRepository();
            return _dBRepository.GetProducts();
        }

        public DataSet GetCategories()
        {
            _dBRepository = new DBRepository();
            return _dBRepository.GetCategories();
        }

        public int Login(string name, string Password)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.Login(name, Password);
        }

        public bool Register(string Fname, string Lname, string email, string password)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.Register(Fname, Lname, email, password);
        }

        public string IsCustomerExists(string email)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.IsCustomerExists(email);
        }

        public int submitOrder(int Productid, string OrderNumber, int CustomerId)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.submitOrder(Productid, OrderNumber, CustomerId);
        }
        public DataTable GetProductyId(int id) 
        {
            _dBRepository = new DBRepository();
            return _dBRepository.GetProductyId(id);
        }

        public int AddProduct(decimal price, string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.AddProduct(price,productName,color,description,screenSize,categoryId,brandName,processor, available);
        }

        public bool EditProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor, bool available,int productID)
        {
            _dBRepository = new DBRepository();
            return _dBRepository.EditProduct(price,productName, color, description, screenSize, categoryId, brandName, processor, available,productID);
        }

        public void AddProductImage(int prodId, string imageUrl)
        {
            _dBRepository = new DBRepository();
            _dBRepository.AddProductImage(prodId, imageUrl);
        }
    }
}
