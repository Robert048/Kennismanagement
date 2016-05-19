using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

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
        public object logIn(string userName, string password)
        {
            try
            {
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

        public object deleteEmployer(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                wqdb.Employers.Remove(wqdb.Employers.First(x => x.ID == ID));
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
