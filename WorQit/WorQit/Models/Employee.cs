using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorQit.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string industry { get; set; }
        public string specialities { get; set; }
        public string positions { get; set; }
        public string interests { get; set; }
        public string languages { get; set; }
        public string skills { get; set; }
        public string educations { get; set; }
        public string volunteer { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string location { get; set; }
        public Nullable<int> hours { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
