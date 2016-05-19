using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorQitService.Models;

namespace WorQitService.Controllers
{
    public class EmployeesController : System.Web.Http.ApiController
    {
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