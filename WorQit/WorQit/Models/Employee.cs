using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorQit.Models
{
    [JsonObject]
    public class Employee
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("industry")]
        public string industry { get; set; }
        [JsonProperty("positions")]
        public string positions { get; set; }
        [JsonProperty("interests")]
        public string interests { get; set; }
        [JsonProperty("languages")]
        public string languages { get; set; }
        [JsonProperty("skills")]
        public string skills { get; set; }
        [JsonProperty("educations")]
        public string educations { get; set; }
        [JsonProperty("dob")]
        public Nullable<System.DateTime> dob { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("hours")]
        public Nullable<int> hours { get; set; }
        [JsonProperty("username")]
        public string username { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("experience")]
        public string experience { get; set; }
    }
}
