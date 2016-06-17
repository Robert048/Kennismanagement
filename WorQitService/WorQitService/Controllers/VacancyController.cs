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

        /// <summary>
        /// sets rating of vacancyemployee (like / dislike)
        /// </summary>
        /// <returns></returns>
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
        /// gets all vacancyemployee objects that have been liked
        /// </summary>
        /// <param name="ID">employer id</param>
        /// <returns></returns>
        public List<VacancyEmployee> getAllLikes(int ID)
        {
            try
            {
                List<Vacancy> valist = getVacancies(ID);
                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;
                List<VacancyEmployee> vaemps = new List<VacancyEmployee>();
                foreach(Vacancy va in valist)
                {
                    var lists = new List<VacancyEmployee>(from VacancyEmployee in wqdb.VacancyEmployees
                                                  where VacancyEmployee.vacancyID == va.ID 
                                                  select VacancyEmployee).ToList();
                    foreach(VacancyEmployee ls in lists)
                    {
                        vaemps.Add(ls);
                    }
                }



                return vaemps;

            }
            catch (Exception ex)
            {
                return null;

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
        /// <param name="ID">employeeID</param>
        /// <param name="hours">minumum hours</param>
        /// <param name="salary">minumum salary</param>
        /// <returns>vacancy list</returns>
        [HttpGet]
        public object getVacanciesByScore(int ID, int salary = 0, int hours = 0)
        {
            try
            {

                WorQitEntities wqdb = new WorQitEntities();
                wqdb.Configuration.ProxyCreationEnabled = false;

                var vacancies = new List<Vacancy>(from VacancyEmployee in wqdb.VacancyEmployees
                                                where VacancyEmployee.employeeID == ID && VacancyEmployee.rating == 0
                                                orderby VacancyEmployee.matchingValue descending
                                               select VacancyEmployee.Vacancy).ToList();
                

                if (salary != 0)
                {
                    vacancies = vacancies.Where(v => v.salary >= salary).ToList();
                }
                if (hours != 0)
                {
                    vacancies = vacancies.Where(v => v.hours >= hours).ToList();
                }

               

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

        /// <summary>
        /// deletes vacancy 
        /// </summary>
        /// <param name="ID">vacancy id</param>
        /// <returns></returns>
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

        /// <summary>
        /// matching algorythm
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="vacancyID"></param>
        private void setMatchScore(int employeeID, int vacancyID)
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
                    if (v.branche.Contains(employee.industry)) matchScore = matchScore + 10;
                    if (v.jobfunction.Contains(employee.positions)) matchScore = matchScore + 5;
                    if (v.requirements.Contains(employee.skills)) matchScore = matchScore + 7;
                    if (v.educations.Contains(employee.educations)) matchScore = matchScore + 9;


                    var ve = wqdb.VacancyEmployees.Where(x => x.employeeID == employee.ID).Where(x => x.vacancyID == v.ID).FirstOrDefault();
                    if(ve == null)
                    {
                        VacancyEmployee newVE = new VacancyEmployee() { employeeID = employee.ID, vacancyID = v.ID, matchingValue = matchScore, rating = 0 };
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

       
        

        /// <summary>
        /// sets score of vacancys for employee
        /// </summary>
        /// <param name="ID">employee ID</param>
        [HttpGet]
        public object setScoreForEmployee(int ID)
        {
            try
            {
                List<Vacancy> vacancyList = getAllVacancies();
                List<Vacancy> newVaList = vacancyList.GroupBy(i => i.employerID).Select(group => group.First()).ToList();

                foreach (Vacancy v in newVaList)
                {
                    setMatchScore(ID, v.ID);
                }

                return Json(new { Result = "successful" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "failed" });
            }
        }


     

    }
}
