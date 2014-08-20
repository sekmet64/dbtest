using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest.Application.Logic
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<string> Hobbies { get; set; }

        public Person()
        {
            Hobbies = new List<string>();
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }        
    }
}
