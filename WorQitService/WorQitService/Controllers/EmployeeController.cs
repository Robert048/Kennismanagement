
using System.Data;
using System.Web.Http;
using System.Linq;
using System;
using WorQitService;
using System.Collections.Generic;

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
            object[] values = { ID, firstName, lastName, industry, specialities, positions, interests, languages, skills, educations, volunteer,
                                     dob, location, hours, username, password, email };
           
            try
            {
                
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                
                
                Employee emp = wqdb.Employees.First(x => x.ID == ID);

                object[] employee = { emp.firstName,
                                        emp.lastName,
                                        emp.industry,
                                        emp.specialities,
                                        emp.positions,
                                        emp.interests,
                                        emp.languages,
                                        emp.skills,
                                        emp.educations,
                                        emp.volunteer,
                                        emp.dob,
                                        emp.location,
                                        emp.hours,
                                        emp.username,
                                        emp.password,
                                        emp.email };
                

                for (int j = 0; j < employee.Length; j++)
                {
                    Console.WriteLine(employee[j]);
                    if (values[j] != null && values[j].ToString() != employee[j].ToString())
                    {
                        employee[j] = values[j];
                        
                    }
                  
                }

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