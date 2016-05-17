
using System.Data;
using System.Web.Http;
using System.Linq;
using System;
using WorQitService;

namespace WorQitService.Controllers
{
    public class EmployeeController : ApiController
    {
        

        /// <summary>
        /// Log in method
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>employee object</returns>
        public object logIn(string userName, string password)
        {
            try
            {
                
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                var values = from Employee in wqdb.Employees
                             where Employee.username == userName
                             select Employee;
                var valuelist = values.ToList<Employee>();
                if (valuelist.Exists(x => x.username == userName))
                {
                    if (password != valuelist[0].password.ToString())
                    {
                        return Json(new { Result = "failed", Error = "Verkeerd wachtwoord" });
                    }
                    else
                    {
                        return Json(new { Result = "successful", User = valuelist });
                    }
                }
                else
                {
                   return Json(new { Result = "failed", Error = "Deze username bestaat niet" });
                }
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object deleteEmployee(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                wqdb.Employees.Remove(wqdb.Employees.First(x => x.ID == ID));
                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object addEmployee(string username, string email, string password)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employee employee = new Employee()
                {
                    username = username,
                    email = email,
                    password = password
                };
                wqdb.Employees.Add(employee);
                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object editEmployee(int ID, string firstName, string lastName, string industry, string specialities, 
            string positions, string interests, string languages, string skills, string educations, string volunteer,
            Nullable<System.DateTime> dob, string location, Nullable<int> hours, string username, string password, string oldPassword, string email)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
               
                Employee emp = wqdb.Employees.First(x => x.ID == ID);
               
                
                emp.firstName = (firstName != null) ? firstName : emp.firstName;
                emp.lastName = (lastName != null) ? lastName : emp.lastName;
                emp.industry = (industry != null) ? industry : emp.industry;
                emp.specialities = (specialities != null) ? specialities : emp.specialities;
                emp.positions = (positions != null) ? positions : emp.positions;
                emp.interests = (interests != null) ? interests : emp.interests;
                emp.languages = (languages != null) ? languages : emp.languages;
                emp.skills = (skills != null) ? skills : emp.skills;
                emp.educations = (educations != null) ? educations : emp.educations;
                emp.volunteer = (volunteer != null) ? volunteer : emp.volunteer;
                emp.dob = (dob != null) ? dob : emp.dob;
                emp.location = (location != null) ? location : emp.location;
                emp.hours = (hours != null) ? hours : emp.hours;
                emp.username = (username != null) ? username : emp.username;
                if (password != null && emp.password == oldPassword)
                {
                    emp.password = password;
                }
                else if (emp.password != oldPassword){
                    return Json (new { Result = "failed", Error = "Verkeerd oud wachtwoord wachtwoord ingevoerd, uw wijzigingen zijn niet opgeslagen" });
                }
                emp.email = (email != null) ? email : emp.email;


                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }


    }
}