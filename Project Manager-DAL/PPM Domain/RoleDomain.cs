using System.Collections.Generic;
using System.Text.RegularExpressions;
using PPM.Model;
using System.Data.SqlClient;

namespace PPM.Domain
{
    public class RoleDomain
    {
        public static List<Role> RoleList = new List<Role>();
        //ConnectionString connectionString = new();
        public void Add(Role role)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "INSERT INTO Roles VALUES (@RoleId,@RoleName)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", role.RoleId);
                    command.Parameters.AddWithValue("@RoleName", role.RoleName);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ViewAll()
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "Select * FROM Roles";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Role robj=new Role();
                            // robj.RoleId = (int)reader["RoleId"];
                            // robj.RoleName = (string)reader["RoleName"];
                            //RoleList.Add(robj);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"RoleId: {reader["RoleId"]}");
                            Console.WriteLine($"RoleName: {reader["RoleName"]}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            //return RoleList;
        }
        public Role GetById(int rId)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "Select * FROM Roles WHERE RoleId=@RoleId";
            Role robj=new Role();
            
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", rId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            reader.Read();
                            robj.RoleId = (int)reader["RoleId"];
                            robj.RoleName = (string)reader["RoleName"];

                        }
                    }
                }
            }
            return robj;
        }
        public void Delete(int rId)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "DELETE FROM Roles WHERE RoleId=@RoleId";
            
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", rId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}