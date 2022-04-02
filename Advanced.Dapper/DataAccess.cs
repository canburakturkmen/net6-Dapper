using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Dapper
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Map Multiple Objects
        public List<Person> GetPeopleWithPhones()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"select pe.*, ph.* from People pe
                                    left join Phones ph
                                        on pe.PhoneId = ph.Id;";
                var people = connection.Query<Person, Phone, Person>(query,
                    (person, phone) => { person.Phone = phone; return person; }).ToList();

                return people;
            }
        }

        //Map Multiple Objects With Paramaters
        public List<Person> GetPeopleWithPhonesByLastName(string lastName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var values = new
                {
                    LastName = lastName
                };

                string query = @"select pe.*, ph.* from People pe
                                    left join Phones ph
                                        on pe.PhoneId = ph.Id
                                    where pe.LastName = @LastName;";
                var people = connection.Query<Person, Phone, Person>(query,
                    (person, phone) => { person.Phone = phone; return person; }, values).ToList();

                return people;
            }
        }

        //Multiple Sets
        public void GetPeopleAndPhones()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                List<Person> people = null;
                List<Phone> phones = null;

                string query = @"select * from People;
                             select * from Phones;";
                
                using (var lists = connection.QueryMultiple(query))
                {
                    //Order matters, must be same with the query
                    people = lists.Read<Person>().ToList();
                    phones = lists.Read<Phone>().ToList();
                }

                foreach (var person in people)
                {
                    Console.WriteLine($"Person: {person.FirstName} {person.LastName}");
                }
                foreach(var phone in phones)
                {
                    Console.WriteLine($"Phone Number: {phone.PhoneNumber}");
                }
            }

        }

        //Multiple Sets With Parameters
        public void GetPeopleAndPhonesByLastNameAndPartialPhoneNumber(string lastName, string partialPhoneNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                List<Person> people = null;
                List<Phone> phones = null;

                string query = @"select * from People
                                  where LastName = @LastName;
                             select * from Phones
                                  where PhoneNumber like '%' + @PartialPhoneNumber + '%';";

                var values = new
                {
                    LastName = lastName,
                    PartialPhoneNumber = partialPhoneNumber
                };
                
                using (var lists = connection.QueryMultiple(query,values))
                {
                    //Order matters, must be same with the query
                    people = lists.Read<Person>().ToList();
                    phones = lists.Read<Phone>().ToList();
                }

                foreach (var person in people)
                {
                    Console.WriteLine($"Person: {person.FirstName} {person.LastName}");
                }
                foreach (var phone in phones)
                {
                    Console.WriteLine($"Phone Number: {phone.PhoneNumber}");
                }
            }
        }

        //Output Paramaters
        public void CreatePersonAndGetId(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var values = new DynamicParameters();

                values.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);
                values.Add("@FirstName", firstName);
                values.Add("@LastName", lastName);

                string query = $@"insert into People (FirstName, LastName)
                                 values(@FirstName, @LastName);
                                 select @Id = @@IDENTITY";

                connection.Execute(query, values);

                int newIdentity = values.Get<int>("@Id");

                Console.WriteLine($"The new Id: {newIdentity}");
            }
        }

        public void CreatePersonWithTransaction(string firstName, string lastName)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var values = new DynamicParameters();
                values.Add("@FirstName", firstName);
                values.Add("@LastName", lastName);

                var query = $@"insert into People (FirstName, LastName) 
                                values(@FirstName, @LastName)";
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    int recordsUpdated = connection.Execute(query, values, transaction);
                    Console.WriteLine($"Records Updated: {recordsUpdated}");

                    try
                    {
                        //Intentional wrong query to get error
                        connection.Execute("update People set Id = 1", transaction: transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {                     
                        Console.WriteLine($"Error: {ex.Message}");
                        //Since we get an error we rollback the transaction
                        transaction.Rollback();
                    }
                }

                Console.WriteLine();
            }
        }

        public void InsertDataSet()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var troopers = GetTroopers();
                var values = new
                {
                    people = troopers.AsTableValuedParameter("BasicUDT")
                };

                int recordsAffected = connection.Execute("dbo.spPerson_InsertSet", values, commandType: CommandType.StoredProcedure);

                Console.WriteLine($"Records affected: {recordsAffected}");
                Console.WriteLine();
            }
        }

        private DataTable GetTroopers()
        {
            var output = new DataTable();

            output.Columns.Add("FirstName", typeof(string));
            output.Columns.Add("LastName", typeof(string));

            output.Rows.Add("Trooper", "12345");
            output.Rows.Add("Trooper", "25412");
            output.Rows.Add("Trooper", "62548");
            output.Rows.Add("Trooper", "95846");
            output.Rows.Add("Trooper", "25846");
            output.Rows.Add("Trooper", "44857");
            output.Rows.Add("Trooper", "95132");
            output.Rows.Add("Trooper", "68426");
            output.Rows.Add("Trooper", "78451");

            return output;
        }

    }
}
