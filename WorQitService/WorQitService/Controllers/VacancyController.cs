using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace WorQitService.Controllers
{
    public class VacancyController : ApiController
    {
        public List<Vacancy> getAllVacancies()
        {
            string connectionString = "Data Source=worqit.database.windows.net;Initial Catalog=WorQit;User id=WorQit; Password=Stenden123";
            string queryString = "SELECT * FROM Vacancy";
            List<Vacancy> list = new List<Vacancy>();
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Vacancy vacancy = new Vacancy(){
                        ID = (int)reader.GetValue(0),
                        employerID = (int)reader.GetValue(1),
                        jobfunction = (string)reader.GetValue(2),
                        description = (string)reader.GetValue(3),
                        salary = (int)reader.GetValue(4),
                        hours = (int)reader.GetValue(5),
                        requirements = (string)reader.GetValue(6),
                        tags = (string)reader.GetValue(7)
                    };
                    list.Add(vacancy);
                }
            }
            return list;
        }
    }
}
