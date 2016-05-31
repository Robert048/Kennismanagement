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

        //[JsonProperty("employerID")]
        public int employerID { get; set; }

        //[JsonProperty("jobfunction")]
        public string jobfunction { get; set; }

        //[JsonProperty("description")]
        public string description { get; set; }

        //[JsonProperty("salary")]
        public int salaray { get; set; }

        //[JsonProperty("hours")]
        public int hours { get; set; }

        //[JsonProperty("requirements")]
        public string requirements { get; set; }

        //[JsonProperty("tags")]
        public string tags { get; set; }

        //[JsonProperty("Employee")]
        public Employee employee { get; set; }
        
    }
}
