using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using WorQitService;
using System.Data;
using System.Web.Http;
using System.Linq;
using System;
using System.Web;

namespace WorQitService.Controllers
{
    public class EmployerController : ApiController
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
                string userName = (headers.Contains("userName")) ? headers.GetValues("userName").First() : null;
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                var values = from Employer in wqdb.Employers
                             where Employer.username == userName
                             select Employer;
                var valuelist = values.ToList<Employer>();
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

        /// <summary>
        /// Log in method for websie
        /// </summary>
        /// <param name="userName"></param>

        /// <returns>employee object</returns>
        public object logInWebsite()
        {
            try
            {
                var headers = Request.Headers;
                string userName = (headers.Contains("userName")) ? headers.GetValues("userName").First() : null;
         
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                var values = from Employer in wqdb.Employers
                             where Employer.username == userName
                             select Employer;
                var valuelist = values.ToList<Employer>();
                if (valuelist.Exists(x => x.username == userName))
                {
                        return Json(new { Result = "successful", User = valuelist });
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
        /// deletes employer
        /// </summary>
        /// <returns></returns>
        public object deleteEmployer()
        {
            try
            {
                var headers = Request.Headers;
                int ID = (headers.Contains("ID")) ? Int32.Parse(headers.GetValues("ID").First()) : -1;
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employer passwordCheck = null;
                try
                {
                    passwordCheck = wqdb.Employers.First(x => x.ID == ID);
                }
                catch(Exception ex) { }
                if (passwordCheck != null)
                {
                    if (passwordCheck.password == password)
                    {
                        wqdb.Employers.Remove(wqdb.Employers.First(x => x.ID == ID));
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
        /// adds employer. Headers: username, email, password
        /// </summary>
        /// <returns></returns>
        public object addEmployer()
        {
            try
            {
                var headers = Request.Headers;
                
                string username = HttpUtility.UrlDecode((headers.Contains("username")) ? headers.GetValues("username").First() : null);
                string email = HttpUtility.UrlDecode((headers.Contains("email")) ? headers.GetValues("email").First() : null);
                string password = HttpUtility.UrlDecode((headers.Contains("password")) ? headers.GetValues("password").First() : null);
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Employer usernameCheck = null;
                Employer emailCheck = null;
                try
                {
                    usernameCheck = wqdb.Employers.First(x => x.username == username);
                    emailCheck = wqdb.Employers.First(x => x.email == email);
                }
                catch (Exception ex)
                {

                }
                if (usernameCheck == null && emailCheck == null)
                {
                    Employer employer = new Employer()
                    {
                        username = username,
                        email = email,
                        password = password
                    };
                    wqdb.Employers.Add(employer);
                    wqdb.SaveChanges();
                    return Json(new { Result = "successful" , employer = employer});
                }
                else
                {
                    string errorString = "";
                    if (usernameCheck != null)
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
                Console.WriteLine(ex);
                return Json(new { Result = "failed", Error = ex });
            }
        }

        /// <summary>
        /// edit employer
        /// </summary>
        /// <returns></returns>
        public object editEmployer()
        {
            try
            {
                var headers = Request.Headers;
                int ID = (headers.Contains("ID")) ? Int32.Parse(headers.GetValues("ID").First()) : -1;
                string name = (headers.Contains("name")) ? headers.GetValues("name").First() : null;
                string description = (headers.Contains("description")) ? headers.GetValues("description").First() : null;
                int employeeCount = (headers.Contains("employeeCount")) ? Int32.Parse(headers.GetValues("employeeCount").First()) : -1;
                
                string location = (headers.Contains("location")) ? headers.GetValues("location").First() : null;
                
                string password = (headers.Contains("password")) ? headers.GetValues("password").First() : null;
                string oldPassword = (headers.Contains("oldPassword")) ? headers.GetValues("oldPassword").First() : null;
                string email = (headers.Contains("email")) ? headers.GetValues("email").First() : null;
                
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;

                Employer emp = wqdb.Employers.First(x => x.ID == ID);

               
                emp.name = (name != null) ? name : emp.name;
                emp.description = (description != null) ? description : emp.description;
                emp.employeeCount = (employeeCount != -1) ? employeeCount : emp.employeeCount;
                
                
                emp.location = (location != null) ? location : emp.location;
                
                
                if (password != null && emp.password == oldPassword)
                {
                    emp.password = password;
                }
                else if (emp.password != oldPassword)
                {
                    return Json(new { Result = "failed", Error = "Verkeerd oud wachtwoord wachtwoord ingevoerd, uw wijzigingen zijn niet opgeslagen" });
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
