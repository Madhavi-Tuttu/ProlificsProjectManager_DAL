using System.Collections.Generic;
using System.Text.RegularExpressions;
using PPM.Model;
using System.Data.SqlClient;


namespace PPM.Domain
{
    public class ValidationCheck
    {
        ProjectDomain projectDomain=new ProjectDomain();
        EmployeeDomain employeeDomain=new EmployeeDomain();
        RoleDomain roleDomain=new RoleDomain();
        string con = "Server = RHJ-9F-D077\\SQLEXPRESS; Database=PPM; Integrated Security=SSPI;";
        public bool CheckProjectId(int pId)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                string cmdText = "SELECT 1 FROM Projects WHERE ProjectId = @ProjectId";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@ProjectId", pId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool ProjectEndDate(DateTime endDate, DateTime startDate)
        {
            if (endDate < startDate)
            {
                return true;
            }
            return false;
        }

        public bool CheckProjectList()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // string tableName = "YourTableName"; // Replace with the actual table name

                string cmdText = "SELECT COUNT(*) FROM Projects";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool CheckEmpToProjectList(int pId)
        {
             using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // string tableName = "YourTableName"; // Replace with the actual table name

                string cmdText = "SELECT COUNT(*) FROM EmployeesInProject";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        public bool CheckEmployeeId(int eId)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                string cmdText = "SELECT 1 FROM Employees WHERE EmployeeId = @EmployeeId";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", eId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool EmployeeEmail(string email)
        {
            Regex emailRegex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=]).{10,}$");
            //var emailexist = EmployeeDomain.EmployeeList.SingleOrDefault(item => item.Email == email);

            if (!emailRegex.IsMatch(email)) //|| emailexist != null)
            {
                return true;
            }
            return false;
        }

        public bool Employeenum(string mNum)
        {
            Regex phoneRegex = new Regex(@"^\+(\d{2})\d{10}$");
            //var numexist = EmployeeDomain.EmployeeList.SingleOrDefault(item => item.MobileNum == mNum);

            if (!phoneRegex.IsMatch(mNum)) // || numexist != null)
            {
                return true;
            }
            return false;
        }
        public bool CheckEmployeeList()
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // string tableName = "YourTableName"; // Replace with the actual table name

                string cmdText = "SELECT COUNT(*) FROM Employees";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool IsEmployeeMappedToProject(int eId)
        {
           using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // string tableName = "YourTableName"; // Replace with the actual table name

                string cmdText = "SELECT COUNT(*) FROM EmployeesToProject WHERE Employee  ";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

        }
        public bool CheckRoleId(int rId)
        {
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                string cmdText = "SELECT 1 FROM Roles WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", rId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public bool CheckRoleList()
        {
           using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                // string tableName = "YourTableName"; // Replace with the actual table name

                string cmdText = "SELECT COUNT(*) FROM Roles";

                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    int rowCount = (int)command.ExecuteScalar();

                    if (rowCount > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool IsRoleMappedToEmployee(int rId)
        {
            return EmployeeDomain.EmployeeList.Any(item => item.EmployeeRoleId == rId);
        }
    }
}