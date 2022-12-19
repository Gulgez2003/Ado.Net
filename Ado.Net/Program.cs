using System;
using System.Data;
using System.Data.SqlClient;

namespace Ado.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Click 1 to see the list of pizzas");
            Console.WriteLine("Click 2 to add pizza");
            Console.WriteLine("Click 0 to exit the program");
            Console.WriteLine();

            int value = int.Parse(Console.ReadLine());
            while (value != 0)
            {
                if (value == 1)
                {
                    GetAllPizzas();
                }
                value = int.Parse(Console.ReadLine());
                if (value == 2)
                {
                    Console.WriteLine("Choose pizza name");
                    string name = Console.ReadLine();
                    InsertPizza(name);
                    Console.WriteLine("Choose ingredient");
                    GetIngredients();
                    int id = int.Parse(Console.ReadLine());
                }
                if (value == 3)
                {
                    Console.WriteLine("Enter Id:");
                    int id = int.Parse(Console.ReadLine());
                    GetPizzaInfo(id);
                    GetPrice(id);
                }
                if (value == 0)
                {
                    break;
                }

            }

        }
        static string ConnectionString = @"Server=WINDOWS-0011A8T;Database=Pizzamizza1;Trusted_Connection=True;";
        public static async void GetAllPizzas()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM  Pizzas", conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id:{reader[0]} Name:{reader[1]} Size:{reader[3]} Price:{reader[4]}");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Click 1 to see the list of pizzas");
                        Console.WriteLine("Click 2 to add pizza");
                        Console.WriteLine("Click 0 to exit the program");
                        Console.WriteLine("If you want to get detailed information about the pizza, click 3, if you don't want it, click 0");
                        Console.WriteLine();
                    }
                }
            };
        }
        public static async void GetIngredients()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Ingredients", conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} {reader[1]}");
                            Console.WriteLine("Enter Id:");
                        }
                    }
                }
            };
        }

        public static async void GetSize()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM  Size", conn))
                {
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id:{reader[0]} Name:{reader[1]}");
                            Console.WriteLine("Enter Id:");
                        }
                    }
                }
            };
        }
        public static async void GetPrice(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT s.Size,p.Price FROM Products pr JOIN Prices p ON pr.Id = p.ProductId JOIN Size s ON p.SizeId = s.Id WHERE pr.Id=@Id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Prices: {reader[0]} {reader[1]}");
                        }
                    }
                }
            };
        }
        public static async void GetPizzaInfo(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [Description] FROM Products", conn))
                {
                    cmd.Parameters.AddWithValue("id", SqlDbType.Int).Value = id;
                    string description = (await cmd.ExecuteScalarAsync()).ToString();
                    Console.WriteLine($"Ingredients: {description}");
                }
            };
        }
        public static async void InsertPizza(string name)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Insert into Products Values(@name)", conn))
                {
                    cmd.Parameters.AddWithValue("name", SqlDbType.NVarChar).Value = name;
                    int result = await cmd.ExecuteNonQueryAsync();
                }
            };
        }
        
    }
}
