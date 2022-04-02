using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Dapper
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }       

        public List<Person> GetPeople()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
               return connection.Query<Person>(
                    $"select * from People").ToList();
            }
        }

        public Person GetPersonById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Person>(
                     $"select * from People where Id = '{id}'");
            }
        }

        public void CreatePerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var values = new DynamicParameters();
                values.Add("@FirstName", person.FirstName);
                values.Add("@LastName", person.LastName);

                var query = $@"insert into People (FirstName, LastName)
                                values(@FirstName,@LastName)";

                connection.Execute(query, values);
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var values = new DynamicParameters();
                values.Add("@Id", person.Id);
                values.Add("@FirstName",person.FirstName);
                values.Add("@LastName", person.LastName);

                var query = $@"update people 
                            set FirstName = @FirstName, LastName = @LastName
                            where Id = @Id";

                connection.Execute(query, values);
            }
        }

        public void DeletePerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var values = new DynamicParameters();
                values.Add("@Id", id);

                var query = $@"delete from people 
                            where Id = @Id";

                connection.Execute(query, values);
            }
        }

    }
}
