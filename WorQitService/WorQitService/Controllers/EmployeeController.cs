using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

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
            string queryString = "SELECT * FROM Employee";
            Employee employee = new Employee();
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
                        employee.ID = Convert.ToInt32(reader.GetValue(0));
                        employee.firstName = reader.GetValue(1).ToString();
                    }
                }
            }
            return Json(new { Result = result, Gerbuiker = employee });
        }

        [HttpGet]
        [ActionName("GetEmployeeByID")]
        public Employee Get(int id)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=tcp:worqit.database.windows.net;Database=WorQit;
            User ID=WorQit@WorQit;Password=Stenden123;Trusted_Connection=False;
            Encrypt=True;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Employee where ID=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.ID = Convert.ToInt32(reader.GetValue(0));
                emp.firstName = reader.GetValue(1).ToString();
            }
            return emp;
            myConnection.Close();
        }

        [HttpPost]
        [ActionName("Add")]
        public void AddEmployee(string name)
        {

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=tcp:worqit.database.windows.net;Database=WorQit;
                User ID=WorQit@WorQit;Password=Stenden123;Trusted_Connection=False;Encrypt=True;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Employee (FirstName) Values (@Name)";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@Name", name);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}