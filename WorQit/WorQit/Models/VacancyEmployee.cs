using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorQit.Models
{
    class VacancyEmployee
    {
        public int matchID { get; set; }
        public int employeeID { get; set; }
        public int vacancyID { get; set; }
        public int rating { get; set; }
        public int matchingValue { get; set; }
        public string reaction { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
