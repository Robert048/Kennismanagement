using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WorQitService.Controllers
{
    public class VacancyController : ApiController
    {

        /// <summary>
        /// Get all vacancys
        /// </summary>
        /// <returns>vacancy list</returns>
        public List<Vacancy> getAllVacancies()
        {
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;
            return wqdb.Vacancies.ToList();

        }

        public object setRating()
        {
            try
            {
                var headers = Request.Headers;
                int employeeID = (headers.Contains("employeeID")) ? Int32.Parse(headers.GetValues("employeeID").First()) : -1;
                int vacancyID = (headers.Contains("vacancyID")) ? Int32.Parse(headers.GetValues("vacancyID").First()) : -1;
                int rating = (headers.Contains("rating")) ? Int32.Parse(headers.GetValues("rating").First()) : -99;


                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                VacancyEmployee vac = (from VacancyEmployee in wqdb.VacancyEmployees
                                             where VacancyEmployee.employeeID == employeeID && VacancyEmployee.vacancyID == vacancyID
                                             select VacancyEmployee).First();
                vac.rating = rating;

                wqdb.SaveChanges();
                return Json(new { Result = "successful"});

            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            
            }
        }

        /// <summary>
        /// Get all candidates on a vacancy
        /// </summary>
        /// <param>vacancyID</param>
        /// <returns>vacancy list</returns>
        public object getCandidates(int ID)
        {
            try
            {
               
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
             
                var vaEmps = new List<Employee>(from VacancyEmployee in wqdb.VacancyEmployees
                                                where VacancyEmployee.vacancyID == ID && VacancyEmployee.rating == 1
                                                select VacancyEmployee.Employee).ToList();


                return Json(new { Result = "successful", Users = vaEmps });

            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            
            }
        }

        /// <summary>
        /// Get all vacancys with score
        /// </summary>
        /// <param>employeeID</param>
        /// <returns>vacancy list</returns>
        [HttpGet]
        public object getVacanciesByScore(int ID)
        {
            try
            {

                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;

                var vacancies = new List<Vacancy>(from VacancyEmployee in wqdb.VacancyEmployees
                                                where VacancyEmployee.employeeID == ID && VacancyEmployee.rating == 0
                                                orderby VacancyEmployee.matchingValue descending
                                               select VacancyEmployee.Vacancy).ToList();


                return Json(new { Result = "successful", Vacancys = vacancies });

            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
               
            }
        }

        /// <summary>
        /// Get all results from VacancyEmployees
        /// </summary>
        /// <returns>vacancyEmployees list</returns>
        public List<VacancyEmployee> getAllVacancyEmployees()
        {
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;
            return wqdb.VacancyEmployees.ToList();
        }

        /// <summary>
        /// Get vacancies from employer
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>vacancy list</returns>
        public List<Vacancy> getVacancies(int ID)
        {
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;
            List<Vacancy> list = new List<Vacancy>();
            foreach (var item in wqdb.Vacancies)
            {
                if (item.employerID == ID) list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Get vacancys with specified requirements
        /// </summary>
        /// <param name="function"></param>
        /// <param name="salary"></param>
        /// <param name="hours"></param>
        /// <param name="requirements"></param>
        /// <param name="tags"></param>
        /// <param name="location"></param>
        /// <returns>vacancy list</returns>
        public List<Vacancy> getVacancies(string function = null, int salary  = 0, int hours = 0 , string requirements = null, string tags = null, string location = null)
        {
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;

            var test = wqdb.Vacancies;
            List<Vacancy> alles = wqdb.Vacancies.ToList();
            if (function != null)
            {
                alles = alles.Where(v => v.jobfunction.Contains(function)).ToList();
            }
            if (salary != 0)
            {
                alles = alles.Where(v => v.salary.Equals(salary)).ToList();
            }
            if (hours != 0)
            {
                alles = alles.Where(v => v.hours.Equals(hours)).ToList();
            }
            if (requirements != null)
            {
                alles = alles.Where(v => v.requirements.Contains(requirements)).ToList();
            }
            if (tags != null)
            {
                alles = alles.Where(v => v.requirements.Contains(tags)).ToList();
            }
            return alles;
        }

        /// <summary>
        /// Create new Vacancy
        /// </summary>
        /// <param name="employerID"></param>
        /// <param name="function"></param>
        /// <param name="description"></param>
        /// <param name="salary"></param>
        /// <param name="hours"></param>
        /// <param name="requirements"></param>
    
        /// <returns>json sucessfull or failed with error</returns>
        public object addVacancy(int employerID, string function, string description, int salary, int hours, string requirements)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                Vacancy vacancy = new Vacancy()
                {
                    employerID = employerID,
                    jobfunction = function,
                    description = description,
                    salary = salary,
                    hours = hours,
                    requirements = requirements
                };
                wqdb.Vacancies.Add(vacancy);
                wqdb.SaveChanges();
                return Json(new { Result = "successful"});
            }
            catch(System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public object deleteVacancy(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                wqdb.Vacancies.Remove(wqdb.Vacancies.First(x => x.ID == ID));
                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (System.Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }

        public void setMatchScore(int employeeID, int vacancyID)
        {
            int bedrijfsScore = 0;
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;
            Vacancy vacancy = wqdb.Vacancies.Where(x => x.ID == vacancyID).First();
            Employee employee = wqdb.Employees.Where(x => x.ID == employeeID).First();
            List<Vacancy> vacancies = wqdb.Vacancies.Where(x => x.employerID == vacancy.employerID).ToList();
            foreach (Vacancy v in vacancies)
            {
                List<VacancyEmployee> vList = wqdb.VacancyEmployees.Where(x => x.vacancyID == v.ID).ToList();
                foreach (VacancyEmployee ve in vList)
                {
                    bedrijfsScore = ve.rating ?? default(int);
                }
                
                if (bedrijfsScore > -5)
                {
                    int matchScore = 0;
                    if (employee.industry == v.jobfunction) matchScore = matchScore + 5;
                   
                    if (employee.industry == v.requirements) matchScore = matchScore + 2;
                    var ve = wqdb.VacancyEmployees.Where(x => x.employeeID == employee.ID).Where(x => x.vacancyID == v.ID).FirstOrDefault();
                    if(ve == null)
                    {
                        VacancyEmployee newVE = new VacancyEmployee() { employeeID = employee.ID, vacancyID = v.ID, matchingValue = matchScore };
                        wqdb.VacancyEmployees.Add(newVE);
                        wqdb.SaveChanges();
                    }
                    else
                    {
                        ve.matchingValue = matchScore;
                    }
                }
            }
        }

        /// <summary>
        /// update seen to true, the like on the vacancy has been seen by the employer
        /// </summary>
        /// <param name="ID">matchID</param>
        public object reactionSeen(int ID)
        {
            try
            {
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                VacancyEmployee vaemp = (from VacancyEmployee in wqdb.VacancyEmployees
                               where VacancyEmployee.matchID == ID
                               select VacancyEmployee).First();
                vaemp.seen = true;

                wqdb.SaveChanges();
                return Json(new { Result = "successful" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed", Error = ex });
            }
        }




    }
}
