using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ClothingShopManagementSystem
{
    class AdminAddProductsData
    {
         public int ID { set; get; } // 0
         public string ProductID { set; get; } // 1
        public string ProductName { set; get; } // 2
        public string Type { set; get; } // 3
        public string Stock { set; get; } // 4
        public string Price { set; get; } // 5
        public string Status { set; get; } // 6
        public string Image { set; get; } // 7
        public string DateInsert { set; get; } // 8
        public string DateUpdate { set; get; } // 9

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hp\OneDrive\Documents\CShop.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");
    
        public List<AdminAddProductsData> productsListData()
        {
            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE date_delete IS NULL";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();

                            apd.ID = (int)reader["Id"];
                            apd.ProductID = reader["ProductID"].ToString();
                            apd.ProductName = reader["ProductName"].ToString();
                            apd.Type = reader["Type"].ToString();
                            apd.Stock = reader["Stock"].ToString();
                            apd.Price = reader["Price"].ToString();
                            apd.Status = reader["Status"].ToString();
                            apd.Image = reader["Image"].ToString();
                            apd.DateInsert = reader["DateInsert"].ToString();
                            apd.DateUpdate = reader["DateUpdate"].ToString();

                            Console.WriteLine($"Product: {apd.ProductName}, Status: {apd.Status}");

                            listData.Add(apd);
                        }

                        //reader.Close();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Failed connection: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }

            return listData;
        }

        public List<AdminAddProductsData> availableProductsData()
        {
            List<AdminAddProductsData> listData = new List<AdminAddProductsData>();

            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();

                    string selectData = "SELECT * FROM products WHERE Status = @stats";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@stats", "Available");

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            AdminAddProductsData apd = new AdminAddProductsData();

                            apd.ID = (int)reader["Id"];
                            apd.ProductID = reader["ProductID"].ToString();
                            apd.ProductName = reader["ProductName"].ToString();
                            apd.Type = reader["Type"].ToString();
                            apd.Stock = reader["Stock"].ToString();
                            apd.Price = reader["Price"].ToString();

                            listData.Add(apd);
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Failed Connection: " + ex);
                }
                finally
                {
                    connect.Close();
                }
            }
            return listData;
        }

    }
}
