using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorQit.Models
{
    public class Vacancy
    {
        public int ID { get; set; }
        public int employerID { get; set; }
        public string jobfunction { get; set; }
        public string description { get; set; }
        public int salary { get; set; }
        public int hours { get; set; }
        public string requirements { get; set; }
        public string branche { get; set; }
        public string educations { get; set; }
        public string location { get; set; }
        public object Employer { get; set; }
        public List<object> VacancyEmployees { get; set; }
    }

    public class VacancyRootObject
    {
        public string Result { get; set; }
        public List<Vacancy> Vacancys { get; set; }
    }
}
