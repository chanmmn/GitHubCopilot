using Microsoft.Data.SqlClient;
using System;


namespace ConAppDemo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new product
            CreateProduct("New Product", 10.99m, 100);

            // Read all products
            ReadProducts();

            // Update a product
            //UpdateProduct(1, "Updated Product", 15.99m, 50);
            UpdateProduct("New Product", 15.99m, 50);

            // Delete a product
            DeleteProduct("New Product");
        }

        public static void CreateProduct(string name, decimal price, int quantity)
        {
            string connectionString = "Server=.;Database=Northwind;Integrated Security=True;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Products (ProductName, UnitPrice, UnitsInStock) VALUES (@Name, @Price, @Quantity)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) inserted.");
                }
            }
        }

        public static void ReadProducts()
        {
            string connectionString = "Server=.;Database=Northwind;Integrated Security=True;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int productId = reader.GetInt32(0);
                            string productName = reader.GetString(1);
                            decimal unitPrice = reader.GetDecimal(2);
                            int unitsInStock = reader.GetInt16(3);

                            Console.WriteLine($"Product ID: {productId}, Name: {productName}, Price: {unitPrice}, Quantity: {unitsInStock}");
                        }
                    }
                }
            }
        }

        //public static void UpdateProduct(int productId, string name, decimal price, int quantity)
        public static void UpdateProduct(string name, decimal price, int quantity)
        {
            string connectionString = "Server=.;Database=Northwind;Integrated Security=True;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string query = "UPDATE Products SET ProductName = @Name, UnitPrice = @Price, UnitsInStock = @Quantity WHERE ProductID = @ProductID";
                string query = "UPDATE Products SET ProductName = @Name, UnitPrice = @Price, UnitsInStock = @Quantity WHERE ProductName = @Name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    //command.Parameters.AddWithValue("@ProductID", productId);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) updated.");
                }
            }
        }

        //public static void DeleteProduct(int productId)
        public static void DeleteProduct(string name)
        {
            string connectionString = "Server=.;Database=Northwind;Integrated Security=True;TrustServerCertificate=true";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                string query = "DELETE FROM Products WHERE ProductName = @Name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@ProductID", productId);
                    command.Parameters.AddWithValue("@Name", name);
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} row(s) deleted.");
                }
            }
        }
    }
}
