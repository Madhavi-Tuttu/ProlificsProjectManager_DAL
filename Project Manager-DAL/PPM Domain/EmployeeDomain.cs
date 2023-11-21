using System.Collections.Generic;
using System.Text.RegularExpressions;
using PPM.Model;
using System.Data.SqlClient;

namespace PPM.Domain
{
    public class EmployeeDomain
    {
        Employee employee = new Employee();
        Project project = new();
        public static List<Employee> EmployeeList = new List<Employee>();
        //ConnectionString connectionString = new();
        public void Add(Employee employee)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "INSERT INTO Employees VALUES (@EmployeeId,@FirstName,@LastName,@Email,@MobileNum,@Address,@EmployeeRoleId)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                 
                    command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Email",employee.Email);
                    command.Parameters.AddWithValue("@MobileNum", employee.MobileNum);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@EmployeeRoleId", employee.EmployeeRoleId);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void ViewAll()
        {
            
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "Select * FROM Employees";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Employee eobj = new Employee();
                            // eobj.EmployeeId = (int)reader["EmployeeId"];
                            // eobj.FirstName = (string)reader["FirstName"];
                            // eobj.LastName = (string)reader["LastName"];
                            // eobj.Email=(string)reader["Email"];
                            // eobj.MobileNum = (string)reader["MobileNum"];
                            // eobj.Address = (string)reader["Address"];
                            // eobj.EmployeeRoleId = (int)reader["EmployeeRoleId"];
                            // EmployeeList.Add(eobj);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"\nEmployeeId: {reader["EmployeeId"]}");
                            Console.WriteLine($"FirstName: {reader["FirstName"]}");
                            Console.WriteLine($"LastName: {reader["LastName"]}");
                            Console.WriteLine($"Email: {reader["Email"]}");
                            Console.WriteLine($"MobileNum: {reader["MobileNum"]}");
                            Console.WriteLine($"Address: {reader["Address"]}");
                            Console.WriteLine($"EmployeeRoleId: {reader["EmployeeRoleId"]}");
                            Console.WriteLine();
                        }
                    }
                }
            }
            //return EmployeeList;
        }
        public Employee GetById(int eId)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "Select * FROM Employees WHERE EmployeeId=@EmployeeId";

            Employee eobj = new Employee();
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", eId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            reader.Read();
                            eobj.EmployeeId = (int)reader["EmployeeId"];
                            eobj.FirstName = (string)reader["FirstName"];
                            eobj.LastName = (string)reader["LastName"];
                            eobj.Email=(string)reader["Email"];
                            eobj.MobileNum = (string)reader["MobileNum"];
                            eobj.Address = (string)reader["Address"];
                            eobj.EmployeeRoleId = (int)reader["EmployeeRoleId"];
                        }
                    }
                }
            }
            return eobj;

        }
        public void Delete(int eId)
        {
            string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
            string cmdText = "DELETE FROM Employees WHERE EmployeeId=@eId";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", eId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
