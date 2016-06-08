using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorQit.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int employerID { get; set; }
        public int employeeID { get; set; }
        public string sender { get; set; }
        public string text { get; set; }
        public bool? read { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public object Employee { get; set; }
        public object Employer { get; set; }
    }

    public class RootObject
    {
        public string Result { get; set; }
        public List<Message> Messages { get; set; }
    }
}
