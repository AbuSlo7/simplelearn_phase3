using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataAccessHelper
    {
        static SqlConnection sqlConnection;
        static SqlCommand sqlCommand;
        static SqlDataAdapter sqlDataAdapter;
        static DataSet dst;
        public static void CreateConnection()
        {
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-4Q9R5I3\E4UWORLDWIDE;user id=sa;password=hKya^AVMpc*vb@5W;persistsecurityinfo=True;Initial Catalog=LaptopPortal;");
        }

        

        public static void CloseConnection()
        {
            sqlConnection.Close();
        }
        public static void OpenConnection()
        {
            sqlConnection.Open();
        }
        internal static bool Regester(string fname, string lname, string email, string password)
        {
            bool result = false;
            try
            {
                CreateConnection();
                sqlCommand = new SqlCommand("sp_register_customer", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", email);
                sqlCommand.Parameters.AddWithValue("@pass", password);
                sqlCommand.Parameters.AddWithValue("@fname", fname);
                sqlCommand.Parameters.AddWithValue("@lname", lname);
                OpenConnection();
                int _result = sqlCommand.ExecuteNonQuery();
                if (_result != 0)
                {
                    result= true;
                }
            }
            catch(Exception ex)
            {
                result= false;
            }
            CloseConnection();
            return result;
        }

        internal static int AddProduct(decimal price,string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor,bool available)
        {
            CreateConnection();
            sqlCommand = new SqlCommand("sp_add_product", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@name", productName);
            sqlCommand.Parameters.AddWithValue("@color", color);
            sqlCommand.Parameters.AddWithValue("@screen_size", screenSize);
            sqlCommand.Parameters.AddWithValue("@processor", processor);
            sqlCommand.Parameters.AddWithValue("@description", description);
            sqlCommand.Parameters.AddWithValue("@available", available);
            sqlCommand.Parameters.AddWithValue("@brand_name", brandName);
            sqlCommand.Parameters.AddWithValue("@category_id", categoryId);
            sqlCommand.Parameters.AddWithValue("@price", price);
            OpenConnection();
            int prodID = (int)sqlCommand.ExecuteScalar();
            CloseConnection();
            return prodID;
        }

        internal static void AddProductImage(int prodId, string imageUrl)
        {
            string query = $"insert into ProductImgs(product_id,img_url) values({prodId},'{imageUrl}')";
            CreateConnection();
            sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.CommandType = CommandType.Text;
            OpenConnection();
            sqlCommand.ExecuteNonQuery();
            CloseConnection();
        }

        internal static bool EditProduct(decimal price, string productName, string color, string description, string screenSize, int categoryId, string brandName, string processor, bool available,int productId)
        {
            bool result = false;
            try
            {
                CreateConnection();
                sqlCommand = new SqlCommand("sp_edit_product", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@name", productName);
                sqlCommand.Parameters.AddWithValue("@color", color);
                sqlCommand.Parameters.AddWithValue("@screen_size", screenSize);
                sqlCommand.Parameters.AddWithValue("@processor", processor);
                sqlCommand.Parameters.AddWithValue("@description", description);
                sqlCommand.Parameters.AddWithValue("@available", available);
                sqlCommand.Parameters.AddWithValue("@brand_name", brandName);
                sqlCommand.Parameters.AddWithValue("@category_id", categoryId);
                sqlCommand.Parameters.AddWithValue("@product_id", productId);
                sqlCommand.Parameters.AddWithValue("@price", price);
                OpenConnection();
                sqlCommand.ExecuteNonQuery();
                CloseConnection();
                result = true;
            }
            catch(Exception ex)
            {
                result = false;
            }
            
            return result;
        }

        internal static DataSet GetCategories()
        {
            string query = "select * from Category";
            CreateConnection();
            sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
            OpenConnection();
            dst = new DataSet();
            sqlDataAdapter.Fill(dst, "Category");
            return dst;
        }

        internal static string IsCustomerExists(string email)
        {
            string Email = string.Empty;
            CreateConnection();
            sqlCommand = new SqlCommand("sp_isExists_customer", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@email", email);
            OpenConnection();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Email = reader["email"].ToString();
            }
            return Email;
        }
        public static int Login(string email, string Password) {
            int CustomerId = 0;
            try
            {
                CreateConnection();
                sqlCommand = new SqlCommand("sp_login_info", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", email.ToString());
                sqlCommand.Parameters.AddWithValue("@pass", Password.ToString());
                OpenConnection();
                DataTable dt = new DataTable();
                dt.Load(sqlCommand.ExecuteReader());
                CustomerId = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
                CloseConnection();
            }
            catch
            {

            }
            return CustomerId;
        }
        public static DataSet GetProducts()
        {
            CreateConnection();
            sqlDataAdapter = new SqlDataAdapter("sp_get_products", sqlConnection);
            dst = new DataSet();
            sqlDataAdapter.Fill(dst, "Product");
            return dst;
        }
        public static DataTable GetProductyId(int id) 
        {
            CreateConnection();
            sqlCommand = new SqlCommand("sp_get_product_byId", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@pid", id.ToString());
            OpenConnection();
            DataTable dt = new DataTable();
            dt.Load(sqlCommand.ExecuteReader());
            CloseConnection();
            return dt;
        }
        public static int submitOrder(int Productid, string OrderNumber, int CustomerId)
        {
            CreateConnection();
            sqlCommand = new SqlCommand("sp_submit_order", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@pid", Productid);
            sqlCommand.Parameters.AddWithValue("@OrderNumber", OrderNumber);
            OpenConnection();
            DataTable dt = new DataTable();
            dt.Load(sqlCommand.ExecuteReader());
            int OrderId = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
            CloseConnection();
            int result = AddRelation(OrderId, CustomerId);
            return result;
        }

        private static int AddRelation(int orderId, int customerId)
        {
            CreateConnection();
            sqlCommand = new SqlCommand("sp_add_relation", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@Oid", orderId);
            sqlCommand.Parameters.AddWithValue("@CustomerId", customerId);
            OpenConnection();
            DataTable dt = new DataTable();
            dt.Load(sqlCommand.ExecuteReader());
            int CustunerOrderId = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
            CloseConnection();
            return CustunerOrderId;
        }
    }
}
