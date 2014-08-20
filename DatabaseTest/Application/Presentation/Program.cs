using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NDesk.Options;
using System.Threading;
using DatabaseTest.Application.Logic;

namespace DatabaseTest
{
    class Program
    {
        static Logic logic = new Logic();

        static void Main(string[] args)
        {
            // Show help if no argument is given.
            bool show_help = args.Length == 0;

            bool show_unknown_argument = false;
            string unknown_argument = null;
            
            // Declare the supported arguments and their actions.
            var p = new OptionSet() {
                {
                    "list|l", "List all people and their hobbies",
                    v => ListAllPeople()
                },
                {
                    "filter|f=", "List people only who have a specified hobby",
                    v => ListPeopleWithHobby(v)
                },
                {
                    "help|?|h", "Show this help message", 
                    v => show_help = v != null
                },
                {
                    "<>",
                    v =>
                    {
                        if (!show_unknown_argument)
                        {
                            show_unknown_argument = true;
                            unknown_argument = v;
                        }
                    }
                },
            };


            // Start parsing the console arguments.
            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `DatabaseTest --help' for more information.");
                return;
            }

            if (show_help)
            {
                ShowHelp(p);
            }

            if (show_unknown_argument)
            {
                Console.WriteLine("Unknown command: " + "\"" + unknown_argument + "\".");
                Console.WriteLine("Use the \"-h\" command-line option to see the available commands.");
            }
        }        

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("DatabaseTest Application");
            Console.WriteLine();
            Console.WriteLine("Usage: DatabaseTest command [parameters...]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            p.WriteOptionDescriptions(Console.Out);
        }

        static void ListAllPeople()
        {
            foreach (var person in logic.People)
            {
                Console.WriteLine(person.ToString());

                Console.Write("hobbies: ");
                if (person.Hobbies.Count > 0)
                {
                    for (int i = 0; i < person.Hobbies.Count - 1; i++)
                    {
                        Console.Write(person.Hobbies[i] + ", ");
                    }

                    Console.WriteLine(person.Hobbies[person.Hobbies.Count - 1]);
                }

                Console.WriteLine();
            }
        }
        
        private static void ListPeopleWithHobby(string hobby)
        {
            var peopleWithHobby = logic.GetPeopleWithHobby(hobby);
            if (peopleWithHobby.Count<Person>() == 0)
            {
                Console.WriteLine("Sorry, no results for " + hobby + ".");
            }
            else
            {
                foreach (var person in peopleWithHobby)
                {
                    Console.WriteLine(person);
                }
            }
        }
    }
}
