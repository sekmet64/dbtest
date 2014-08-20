using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DatabaseTest.Application.Data;

namespace DatabaseTest.Application.Logic
{
    class Logic
    {
        private DataAccess dataAccess = new DataAccess();

        public IEnumerable<Person> People
        {
            get
            {
                return dataAccess.People;
            }
        }

        internal IEnumerable<Person> GetPeopleWithHobby(string hobby)
        {
            return dataAccess.GetPeopleWithHobby(hobby);
        }
    }
}
