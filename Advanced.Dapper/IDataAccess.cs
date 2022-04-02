using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Dapper
{
    public interface IDataAccess
    {
        List<Person> GetPeopleWithPhones();
        List<Person> GetPeopleWithPhonesByLastName(string lastName);
        void GetPeopleAndPhones();
        void GetPeopleAndPhonesByLastNameAndPartialPhoneNumber(string lastName, string partialPhoneNumber);
        void CreatePersonAndGetId(string firstName, string lastName);

        void CreatePersonWithTransaction(string firstName, string lastName);
        void InsertDataSet();
    }
}
