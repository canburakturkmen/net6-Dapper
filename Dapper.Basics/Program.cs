using Basics.Dapper;
using Models;

const string connectionString = "Server=.;Database=PersonDB;Trusted_Connection=True;";

IDataAccess dbContext = new DataAccess(connectionString);

//Get People
//var people = dbContext.GetPeople();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}: {person.Phone?.PhoneNumber}");
//}

//Get Person
//var person = dbContext.GetPersonById(1);
//Console.WriteLine($"{person.FirstName} {person.LastName}");

//Insert Person
//var newPerson = new Person { FirstName = "John", LastName = "Doe" };
//dbContext.CreatePerson(newPerson);
//var people = dbContext.GetPeople();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}");
//}

//UpdatePerson
//var toBeUpdatedPerson = dbContext.GetPersonById();
//toBeUpdatedPerson.LastName = "Test";
//dbContext.UpdatePerson(toBeUpdatedPerson);
//var people = dbContext.GetPeople();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}");
//}

//DeletePerson
//dbContext.DeletePerson(1016);
//var people = dbContext.GetPeople();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}");
//}

Console.ReadKey();