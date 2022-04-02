using Advanced.Dapper;
using System.Data;

const string connectionString = "Server=.;Database=PersonDB;Trusted_Connection=True;";

IDataAccess dbContext = new DataAccess(connectionString);

//Map Multiple Objects

//var people = dbContext.GetPeopleWithPhones();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}: Phone: {person.Phone?.PhoneNumber}");
//}

//Map Multiple Objects With Paramaters

//var people = dbContext.GetPeopleWithPhonesByLastName("Gaila");
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}: Phone: {person.Phone?.PhoneNumber}");
//}



//Multiple Sets

//dbContext.GetPeopleAndPhones();



//Multiple Sets With Parameters

//dbContext.GetPeopleAndPhonesByLastNameAndPartialPhoneNumber("Gaila", "205");



//Output Paramater

//dbContext.CreatePersonAndGetId("NewPerson", "NewPerson");



//RunWithTransaction

//dbContext.CreatePersonWithTransaction("test", "test");
//var people = dbContext.GetPeopleWithPhones();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}: Phone: {person.Phone?.PhoneNumber}");
//}



//InserDataSet

//dbContext.InsertDataSet();
//var people = dbContext.GetPeopleWithPhones();
//foreach (var person in people)
//{
//    Console.WriteLine($"{person.FirstName} {person.LastName}: Phone: {person.Phone?.PhoneNumber}");
//}



Console.ReadKey();


