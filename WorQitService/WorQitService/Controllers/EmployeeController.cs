using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;

using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity.Core.Objects;

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
            string connectionString = "Data Source=worqit.database.windows.net;Initial Catalog=WorQit;User id=WorQit; Password=Stenden123";
            string queryString = "SELECT * FROM Employee WHERE Employee.username = @username";
            Employee employee = new Employee();
            bool result = false;
            object[] values = new object[25];
            //object[] keys = new object[17];
            var keys = new List<string>();
          
            Dictionary<string,object> keyvalues = new Dictionary<string, object>();
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@username", SqlDbType.VarChar);
                command.Parameters["@username"].Value = userName;
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    keys = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList<string>();
                    reader.GetValues(values);  // gets all values of SELECT statement and puts it into values array
                    values.ToList<object>();
                    keyvalues = keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

                    if (password == keyvalues["passwod"].ToString())
                    {
                       
                        result = true;
                    }
                }
                return Json(new { Result = result, Gerbuiker = values, Keys = keys, KV = keyvalues });
            }
            
        }

        //public object login1(string userName, string password)
        //{
        //    string gebruiker;
        //    using (ObjectContext objC = new ObjectContext("name=WorQitService.Employee"))
        //    {
        //            ObjectQuery<Employee> employees =

        //             objC.CreateQuery<Employee>("WorQitService.Employee");



        //            foreach (Employee employee in employees)

        //            {

        //                Console.WriteLine(employee.firstName);
        //            gebruiker = employee.firstName;

        //            }
        //    }

        //        return Json(new { Result = result, Gerbuiker = gebruiker });
        //}
    }
}