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
