using System.Collections.Generic;
using System.Text.RegularExpressions;
using PPM.Model;
using System.Data.SqlClient;

namespace PPM.Domain
{
    public class ProjectDomain
    {
        Project project = new Project();
        public static List<Project> ProjectList = new List<Project>();
        EmployeeDomain employeeDomain = new EmployeeDomain();
        string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
        public void Add(Project project)
        {
            string cmdText = "INSERT INTO Projects VALUES (@ProjectId,@ProjectName,@StartDate,@EndDate)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                    command.Parameters.AddWithValue("@projectName", project.ProjectName);
                    command.Parameters.AddWithValue("@StartDate", project.StartDate);
                    command.Parameters.AddWithValue("@EndDate", project.EndDate);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ViewAll()
        {
            string cmdText = "Select * FROM Projects";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Project pobj = new Project();
                            // pobj.ProjectId = (int)reader["ProjectId"];
                            // pobj.ProjectName = (string)reader["ProjectName"];
                            // pobj.StartDate = (DateTime)reader["StartDate"];
                            // pobj.EndDate = (DateTime)reader["EndDate"];
                            // ProjectList.Add(pobj);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"ProjectID: {reader["ProjectID"]}");
                            Console.WriteLine($"ProjectName: {reader["ProjectName"]}");
                            Console.WriteLine($"StartDate: {reader["StartDate"]}");
                            Console.WriteLine($"EndDate: {reader["EndDate"]}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        // return ProjectList;
        public Project GetById(int pId)
        {
            string cmdText = "Select * FROM Projects WHERE ProjectId=@ProjectId";
            Project pobj = new Project();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            reader.Read();
                            pobj.ProjectId = (int)reader["ProjectId"];
                            pobj.ProjectName = (string)reader["ProjectName"];
                            pobj.StartDate = (DateTime)reader["StartDate"];
                            pobj.EndDate = (DateTime)reader["EndDate"];
                        }
                    }
                }
            }
            return pobj;

        }
        public void Delete(int pId)
        {
            string cmdText = "DELETE FROM Projects WHERE ProjectId=@pId";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddEmpToProject(int pId, int eId)
        {
            string cmdText = "INSERT INTO EmployeesInProject VALUES(@ProjectId,@EmployeeId)";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);
                    command.Parameters.AddWithValue("@EmployeeId", eId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void RemoveEmpFromProject(int pId, int eId)
        {
            string cmdText = "DELETE FROM EmployeesInProject WHERE ProjectId=@ProjectId AND EmployeeId=@EmployeeId";

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);
                    command.Parameters.AddWithValue("@EmployeeId", eId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ViewEmployeeInProject(int pId)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                string query = "SELECT E.EmployeeId, E.FirstName, E.LastName, E.Email, E.MobileNum, E.Address, E.EmployeeRoleId " +
                               "FROM Employees E " +
                               "JOIN EmployeesInProject EP ON E.EmployeeId = EP.EmployeeId " +
                               "WHERE EP.ProjectId = @ProjectId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {   
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Employees in Project {pId}:");
                        while (reader.Read())
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"\nEmployeeId: {reader["EmployeeId"]}");
                            Console.WriteLine($"FirstName: {reader["FirstName"]}");
                            Console.WriteLine($"LastName: {reader["LastName"]}");
                            Console.WriteLine($"Email: {reader["Email"]}");
                            Console.WriteLine($"MobileNum: {reader["MobileNum"]}");
                            Console.WriteLine($"Address: {reader["Address"]}");
                            Console.WriteLine($"EmployeeRoleId: {reader["EmployeeRoleId"]}");
                            Console.WriteLine();
                            // if (reader.HasRows == true)
                            // {
                            //     reader.Read();
                            //     Employee eobj = new Employee();
                            //     eobj.EmployeeId = (int)reader["EmployeeId"];
                            //     eobj.FirstName = (string)reader["FirstName"];
                            //     eobj.LastName = (string)reader["LastName"];
                            //     eobj.Email = (string)reader["Email"];
                            //     eobj.MobileNum = (string)reader["MobileNum"];
                            //     eobj.Address = (string)reader["Address"];
                            //     eobj.EmployeeRoleId = (int)reader["EmployeeRoleId"];
                            //     EmployeeDomain.EmployeeList.Add(eobj);
                            // }
                        }

                    }
                    //return EmployeeDomain.EmployeeList;
                }
            }
        }
    }
}

