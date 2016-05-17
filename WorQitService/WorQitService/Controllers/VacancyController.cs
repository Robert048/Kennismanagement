using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WorQitService.Controllers
{
    public class VacancyController : ApiController
    {
        public List<Vacancy> getAllVacancies()
        {
            WorQitEntities wqdb = new WorQitEntities();
            wqdb.Configuration.ProxyCreationEnabled = false;
            List<Vacancy> list = new List<Vacancy>();
            foreach (var item in wqdb.Vacancies)
            {
               list.Add(item);
            }
            return list;
        }

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

        public object addVacancy(int employerID, string function, string description, int salary, int hours, string requirements, string tags)
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
                    requirements = requirements,
                    tags = tags
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
    }
}
