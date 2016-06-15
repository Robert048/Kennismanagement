
using System.Data;
using System.Web.Http;
using System.Linq;
using System;
using System.Web;
using WorQitService;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using System.Globalization;

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
        public object logIn()
        {
            try
            {
                var headers = Request.Headers;
                string username = (headers.Contains("userName")) ? headers.GetValues("userName").First() : null;
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                var values = from Employee in wqdb.Employees
                             where Employee.username == username
                             select Employee;
                var valuelist = values.ToList<Employee>();
                if (valuelist.Exists(x => x.username == username))
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

        /// <summary>
        /// deletes employee
        /// </summary>
        /// <returns></returns>
        public object deleteEmployee()
        {
            try
            {
                var headers = Request.Headers;
                int ID = (headers.Contains("ID")) ? Int32.Parse(headers.GetValues("ID").First()) : -1;
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employee passwordCheck = null;
                try
                {
                    passwordCheck = wqdb.Employees.First(x => x.ID == ID);
                }
                catch (Exception ex) { }
                if (passwordCheck != null)
                {
                    if (passwordCheck.password == password)
                    {
                        wqdb.Employees.Remove(wqdb.Employees.First(x => x.ID == ID));
                        wqdb.SaveChanges();
                        return Json(new { Result = "successful" });
                    }
                    else
                    {
                        return Json(new { Result = "failed", Error = "Verkeerd wachtwoord" });
                    }
                }
                else
                {
                    return Json(new { Result = "failed", Error = "Deze user bestaat niet" });
                }
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        /// <summary>
        /// adds employee
        /// </summary>
        /// <returns></returns>
        public object addEmployee()
        {
            try
            {
                
                var headers = Request.Headers;
                string username = HttpUtility.UrlDecode((headers.Contains("userName")) ? headers.GetValues("userName").First() : null);
                string email = HttpUtility.UrlDecode((headers.Contains("email")) ? headers.GetValues("email").First() : null);
                string password = HttpUtility.UrlDecode((headers.Contains("password")) ? headers.GetValues("password").First() : null);
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employee usernameCheck = null;
                Employee emailCheck = null;
                try
                {
                    usernameCheck = wqdb.Employees.First(x => x.username == username );
                    emailCheck = wqdb.Employees.First(x => x.email == email);
                }
                catch (Exception ex)
                {

                }
                if (usernameCheck == null && emailCheck == null)
                {
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
                else
                {
                    string errorString = "";
                    if(usernameCheck != null)
                    {
                        errorString += "De gebruikersnaam bestaat al";
                    }
                    if (emailCheck != null)
                    {
                        errorString += "dit email adres wordt al gebruikt";
                    }
                    return Json(new { Result = "failed", Error = errorString });
                }
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        /// <summary>
        /// edits employee
        /// </summary>
        /// <returns></returns>
        public object editEmployee()
        {
            try
            {
                var headers = Request.Headers;
                int ID = (headers.Contains("ID")) ? Int32.Parse(headers.GetValues("ID").First()) : -1;

                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employee emp = wqdb.Employees.First(x => x.ID == ID);

                
                string firstName = (headers.Contains("firstName")) ? headers.GetValues("firstName").First() : null;
                string lastName = (headers.Contains("lastName")) ? headers.GetValues("lastName").First() : null;
                string industry = (headers.Contains("industry")) ? headers.GetValues("industry").First() : null;
                
                string positions = (headers.Contains("positions")) ? headers.GetValues("positions").First() : null;
                string interests = (headers.Contains("interests")) ? headers.GetValues("interests").First() : null;
                string languages = (headers.Contains("languages")) ? headers.GetValues("languages").First() : null;
                string skills = (headers.Contains("skills")) ? headers.GetValues("skills").First() : null;
                string educations = (headers.Contains("educations")) ? headers.GetValues("educations").First() : null;
                string experience = (headers.Contains("experience")) ? headers.GetValues("experience").First() : null; // werkervaring
                

                if (headers.Contains("dob") && headers.GetValues("dob").First() != null) {

                    DateTime? dob = DateTime.Parse(headers.GetValues("dob").First().ToString());
                    //return Json(new { Result = "lolz dob", troep = dob });
                    emp.dob = (dob != null) ? dob : emp.dob;
                    
                }
                string location = (headers.Contains("city")) ? headers.GetValues("city").First() : null;
                if (headers.Contains("hours")) {
                    int? hours = int.Parse(headers.GetValues("hours").First());
                    emp.hours = (hours != null) ? hours : emp.hours;
                }
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                string oldPassword = (headers.Contains("oldPassword")) ? headers.GetValues("oldPassword").First() : null;
                string email = (headers.Contains("email")) ? headers.GetValues("email").First() : null;

                
                
                emp.firstName = (firstName != null) ? firstName : emp.firstName;
                emp.lastName = (lastName != null) ? lastName : emp.lastName;
                emp.industry = (industry != null) ? industry : emp.industry;
              
                emp.positions = (positions != null) ? positions : emp.positions;
                emp.interests = (interests != null) ? interests : emp.interests;
                emp.languages = (languages != null) ? languages : emp.languages;
                emp.skills = (skills != null) ? skills : emp.skills;
                emp.educations = (educations != null) ? educations : emp.educations;
        
                emp.experience = (experience != null) ? experience : emp.experience;
                emp.location = (location != null) ? location : emp.location;
               
                emp.educations = (educations != null) ? educations : emp.educations;


                if (password != null && emp.password == oldPassword)
                {
                    emp.password = password;
                }
                else if (emp.password != oldPassword){
                    return Json (new { Result = "failed", Error = "Verkeerd oud wachtwoord wachtwoord ingevoerd, uw wijzigingen zijn niet opgeslagen" });
                }
                emp.email = (email != null) ? email : emp.email;


                wqdb.SaveChanges();
                return Json(new { Result = "successful", User = emp });
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        


    }
}