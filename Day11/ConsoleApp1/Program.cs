using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectString = "Server=localhost\\SQLEXPRESS;Database=StudentDB;Trusted_Connection=True;";

            using (SqlConnection con = new SqlConnection(connectString))
            {
                con.Open();
                Console.WriteLine("Conneted to Database!");

                string query = "Insert INTO Students (Name, Age) VALUES (@Name, @Age)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", "Tanaya");
                    cmd.Parameters.AddWithValue("@Age", 22);

                    cmd.ExecuteNonQuery();
                }


                Console.WriteLine("Data Inserted Successfully");
                Console.ReadLine();
            }
        }
    }
}
