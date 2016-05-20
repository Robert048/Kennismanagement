
using System.Data;
using System.Web.Http;
using System.Linq;
using System;
using WorQitService;
using System.Threading.Tasks;
using System.Net.Http;

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

        public object addEmployee()
        {
            try
            {
                var headers = Request.Headers;
                string username = (headers.Contains("userName")) ? headers.GetValues("userName").First() : null;
                string email = (headers.Contains("email")) ? headers.GetValues("email").First() : null;
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
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

        public object editEmployee()
        {
            try
            {
                var headers = Request.Headers;
                int ID = (headers.Contains("ID")) ? Int32.Parse(headers.GetValues("ID").First()) : -1;
                string firstName = (headers.Contains("firstName")) ? headers.GetValues("firstName").First() : null;
                string lastName = (headers.Contains("lastName")) ? headers.GetValues("lastName").First() : null;
                string industry = (headers.Contains("industry")) ? headers.GetValues("industry").First() : null;
                string specialities = (headers.Contains("specialities")) ? headers.GetValues("specialities").First() : null;
                string positions = (headers.Contains("positions")) ? headers.GetValues("positions").First() : null;
                string interests = (headers.Contains("interests")) ? headers.GetValues("interests").First() : null;
                string languages = (headers.Contains("languages")) ? headers.GetValues("languages").First() : null;
                string skills = (headers.Contains("skills")) ? headers.GetValues("skills").First() : null;
                string educations = (headers.Contains("educations")) ? headers.GetValues("educations").First() : null;
                string volunteer = (headers.Contains("volunteer")) ? headers.GetValues("volunteer").First() : null;
                Nullable< System.DateTime > dob = (headers.Contains("dob")) ? DateTime.Parse(headers.GetValues("dob").First()) : DateTime.Parse(null);
                string location = (headers.Contains("location")) ? headers.GetValues("location").First() : null;
                Nullable< int > hours = (headers.Contains("hours")) ? Int32.Parse(headers.GetValues("hours").First()) : Int32.Parse(null);
                
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                string oldPassword = (headers.Contains("oldPassword")) ? headers.GetValues("oldPassword").First() : null;
                string email = (headers.Contains("ID")) ? headers.GetValues("ID").First() : null;

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
                emp.dob = (dob != DateTime.Parse(null)) ? dob : emp.dob;
                emp.location = (location != null) ? location : emp.location;
                emp.hours = (hours != Int32.Parse(null)) ? hours : emp.hours;
              
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