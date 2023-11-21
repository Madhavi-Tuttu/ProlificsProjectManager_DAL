using PPM.Domain;
using PPM.Model;

namespace PPM.UI
{
    public class ProjectUI
    {
        //Project project = new Project();
        ProjectDomain projectDomain = new();
        //EmployeeDomain employeeDomain = new EmployeeDomain();
        EmployeeUI employeeUI = new EmployeeUI();
        ValidationCheck validationCheck = new();
        //EmployeesInProject employeesInProject=new EmployeesInProject();

        public void AddProject()
        {
            Project project = new Project();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----------**Add Project**-----------------");
            Console.ForegroundColor = ConsoleColor.White;
        pidlabel:
            Console.WriteLine("Enter Project Id");
            int pId = int.Parse(Console.ReadLine());

            if (validationCheck.CheckProjectId(pId))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Id is already exists");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Id again");
                goto pidlabel;
            }
            project.ProjectId = pId;

            Console.WriteLine("Enter Project name");
            project.ProjectName = Console.ReadLine();
            Console.WriteLine("Enter Project start date");
            DateTime StartDate = DateTime.Parse(Console.ReadLine());
        dlabel:
            Console.WriteLine("Enter Project end date");
            DateTime EndDate = DateTime.Parse(Console.ReadLine());

            //validating project enddate
            if (validationCheck.ProjectEndDate(EndDate, StartDate))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Project End date cannot be less than start date ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter the end date again");
                goto dlabel;
            }
            project.StartDate = StartDate;
            project.EndDate = EndDate;


            projectDomain.Add(project);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nProject Details are added to the list");
            Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine("Do you want to add employee with role to project(Yes/No)");
            //string emprole = Console.ReadLine();


        }

        public void ViewAllProjects()
        {
            if (validationCheck.CheckProjectList())
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------**Displaying Project List**--------------");
                //for loop to retrieve projects from list
                 projectDomain.ViewAll();
                //Project Projectlist= projectDomain.ViewAll();
                Console.ForegroundColor = ConsoleColor.White;
                // foreach (Project p in projectDomain.ViewAll())
                // {
                //     Console.WriteLine("\n{0},{1},{2},{3}", p.ProjectId, p.ProjectName, p.StartDate, p.EndDate);
                // }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Project List is Empty.Add projects to view");
            }
        }
        public void ViewProjectById()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----------**View Project By Id**-----------------");
            Console.ForegroundColor = ConsoleColor.Green;
        projectid:
            Console.WriteLine("Enter Project Id to view project by Id");
            int projectid = int.Parse(Console.ReadLine());

            if (validationCheck.CheckProjectId(projectid))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Displaying Project List By Id");

                Project Getproject = projectDomain.GetById(projectid);
                if (Getproject != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n{0},{1},{2},{3}", Getproject.ProjectId, Getproject.ProjectName, Getproject.StartDate, Getproject.EndDate);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Project Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist project Id");
                goto projectid;
            }
        }
        public void DeleteProject()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("-----------**Delete Project**-----------------");
            Console.ForegroundColor = ConsoleColor.Green;
        projectid:
            Console.WriteLine("Enter Project Id to delete project from Projectlist");
            int projectid = int.Parse(Console.ReadLine());

            if (validationCheck.CheckProjectId(projectid))
            {
                projectDomain.Delete(projectid);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nProject with ProjectId {0} is removed from Project list", projectid);
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Project Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist project Id");
                goto projectid;
            }
        }
        public void AddEmployeeToProject()
        {
        projectid:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter Project Id to add employee to project");
            int projectid = int.Parse(Console.ReadLine());

            if (!validationCheck.CheckProjectId(projectid))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Project Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist project Id");
                goto projectid;
            }

            if (validationCheck.CheckEmployeeList())
            {
                goto peidlabel;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Employee list is empty ,add employee and then add employee to exist project");
                employeeUI.AddEmployee();
            }

        peidlabel:
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Employee Id to add employee to project");
            int employeeid = int.Parse(Console.ReadLine());


            if (!validationCheck.CheckEmployeeId(employeeid))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered employee Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist employee Id again");
                goto peidlabel;
            }

            projectDomain.AddEmpToProject(projectid, employeeid);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee Details are added to the Project list");

        }

        public void ViewEmployeeInProject()
        {

        projectId:
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Project Id to display project details");
            int projectId = int.Parse(Console.ReadLine());

            if (!validationCheck.CheckProjectId(projectId))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Project Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist project Id again to view project details");
                goto projectId;
            }

            if (validationCheck.CheckEmpToProjectList(projectId))
            {   
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("----------**Displaying Employees In Project**--------------");
                projectDomain.ViewEmployeeInProject(projectId);
                // Console.ForegroundColor = ConsoleColor.Blue;
                // Console.WriteLine("Displaying Project Details");

                // Console.ForegroundColor = ConsoleColor.Magenta;
                // Project projects = projectDomain.GetById(projectId);
                // Console.WriteLine("{0},{1},{2},{3}", projects.ProjectId, projects.ProjectName, projects.StartDate, projects.EndDate);

                // foreach (Employee e in projectDomain.ViewEmployeeInProject(projectId))
                // {
                //     Console.ForegroundColor = ConsoleColor.White;
                //     Console.WriteLine("\nDisplaying Employees in Project List");
                //     Console.WriteLine("\n{0},{1},{2},{3},{4},{5}", e.EmployeeId, e.FirstName, e.LastName, e.Email, e.MobileNum, e.Address, e.EmployeeRoleId);

                // }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("EmployeeToProject List is Empty.Employees are not Added to project");
            }
        }
        public void DeleteEmployeeFromProject()
        {
            Project projectobj = new Project();
            Console.ForegroundColor = ConsoleColor.Green;
        projectid:
            Console.WriteLine("Enter Project Id to remove employee from project");
            int projectid = int.Parse(Console.ReadLine());

            if (!validationCheck.CheckProjectId(projectid))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered Project Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist project Id again");
                goto projectid;
            }

        employeeid:
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Enter Employee Id to remove employee from project");
            int employeeid = int.Parse(Console.ReadLine());

            if (!validationCheck.CheckEmployeeId(employeeid))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Entered employee Id is not exist");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter exist employee Id again");
                goto employeeid;
            }

            projectDomain.RemoveEmpFromProject(projectid, employeeid);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nEmployee Details are removed from EmployeeToProject list");

        }
    }
}



