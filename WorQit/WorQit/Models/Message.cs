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
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("employerID")]

        public Nullable<int> employerID { get; set; }
        [JsonProperty("employeeID")]

        public Nullable<int> employeeID { get; set; }
        [JsonProperty("sender")]

        public string sender { get; set; }
        [JsonProperty("text")]

        public string text { get; set; }
        [JsonProperty("read")]

        public Nullable<bool> read { get; set; }
        [JsonProperty("title")]

        public string title { get; set; }

    }
}
