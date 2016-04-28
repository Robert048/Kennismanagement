using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorQitService.Models;

namespace WorQitService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Employee> Get()
        {
            return new List<Employee>()
                {
                    new Employee() {EmployeeID = 1, EmployeeName = "Generic Indian Female" },
                    new Employee() {EmployeeID = 2, EmployeeName = "Generic Indian Male" },
                };
        }

        // GET api/values/5
        public Employee Get(int id)
        {
            if (id == 1)
            {
                return new Employee() { EmployeeID = 1, EmployeeName = "Generic Indian Man" };
            }
            return new Employee() { EmployeeID = 2, EmployeeName = "Generic Indian Man" };
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
