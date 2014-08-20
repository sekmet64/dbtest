using DatabaseTest.Application.Data.PeopleDataSetTableAdapters;
using DatabaseTest.Application.Logic;
using DatabaseTest.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest.Application.Data
{
    public class DataAccess
    {
        public IEnumerable<Person> People
        {
            get
            {
                IList<Person> people = new List<Person>();                

                PeopleDataSet.PeopleDataTable peopleDataTable = new PeopleDataSet.PeopleDataTable();
                var peopleTableAdapter = new PeopleTableAdapter();
                peopleTableAdapter.Fill(peopleDataTable);

                foreach (DataRow row in peopleDataTable.Rows)
                {
                    string firstName = row["FirstName"].ToString();
                    string lastName = row["LastName"].ToString();
                    int id = (int)row["Id"];

                    var person = new Person()
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                    

                    PeopleDataSet.HobbiesDataTable hobbiesDataTable = new PeopleDataSet.HobbiesDataTable();
                    var hobbiesTableAdapter = new HobbiesTableAdapter();
                    hobbiesTableAdapter.FillByPerson(hobbiesDataTable, id);

                    foreach (DataRow hobbyRow in hobbiesDataTable.Rows)
                    {
                        person.Hobbies.Add(hobbyRow["Name"].ToString());
                    }

                    people.Add(person);
                }

                return people;
            }
        }

        internal IEnumerable<Person> GetPeopleWithHobby(string hobby)
        {
            IList<Person> people = new List<Person>();

            PeopleDataSet.PeopleDataTable peopleDataTable = new PeopleDataSet.PeopleDataTable();
            var peopleTableAdapter = new PeopleTableAdapter();
            peopleTableAdapter.FillByHobby(peopleDataTable, hobby);

            foreach (DataRow row in peopleDataTable.Rows)
            {
                string firstName = row["FirstName"].ToString();
                string lastName = row["LastName"].ToString();
                int id = (int)row["Id"];

                var person = new Person()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                people.Add(person);
            }
            
            return people;
        }
    }
}
