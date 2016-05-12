using System;
using System.Data.SqlClient;
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
            string connectionString = "Data Source=worqit.database.windows.net;Initial Catalog=WorQit;User id=WorQit; Password=Stenden123";
            string queryString = "SELECT * FROM Employee";
            Employer employer = new Employer();
            bool result = false;
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (password == reader.GetValue(2).ToString()) ;
                    {
                        employer.ID = Convert.ToInt32(reader.GetValue(0));
                        employer.name = reader.GetValue(1).ToString();
                    }
                }
            }
            return Json(new { Result = result, Gerbuiker = employer });
        }
    }
}
