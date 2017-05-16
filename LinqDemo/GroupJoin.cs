using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqDemo
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }

    class GroupJoin
    {

        public static void ExampleGroupJoin()
        {
            Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
            Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
            Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
            Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };

            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            // Create two lists.
            List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

            // Create a list where each element is an anonymous type
            // that contains the person's first name and a collection of 
            // pets that are owned by them.
            var query = from person in people
                        join pet in pets on person equals pet.Owner into gj
                        select new { OwnerName = person.FirstName, Pets = gj };

            foreach (var v in query)
            {
                // Output the owner's name.
                Console.WriteLine("{0}:", v.OwnerName);
                // Output each of the owner's pet's names.
                foreach (Pet pet in v.Pets)
                    Console.WriteLine("  {0}", pet.Name);
            }

            Console.WriteLine();
            Console.WriteLine();

            query = people.GroupJoin(pets,
                         person => person,
                         pet => pet.Owner,
                         (person, petCollection) =>
                             new
                             {
                                 OwnerName = person.FirstName,
                                 Pets = petCollection
                             });

            foreach (var v in query)
            {
                // Output the owner's name.
                Console.WriteLine("{0}:", v.OwnerName);
                // Output each of the owner's pet's names.
                foreach (Pet pet in v.Pets)
                    Console.WriteLine("  {0}", pet.Name);
            }

            Console.WriteLine();
            Console.WriteLine();

            // Create XML to display the hierarchical organization of people and their pets.
            XElement ownersAndPets = new XElement("PetOwners",
                from person in people
                join pet in pets on person equals pet.Owner into gj
                select new XElement("Person",
                    new XAttribute("FirstName", person.FirstName),
                    new XAttribute("LastName", person.LastName),
                    from subpet in gj
                    select new XElement("Pet", subpet.Name)));

            Console.WriteLine(ownersAndPets);

            ownersAndPets = new XElement("PetOwners",
                            people.GroupJoin(pets, 
                                            person => person,
                                            pet => pet.Owner,
                                            (person, petCollection) => new XElement("Person",
                                                            new XAttribute("FirstName", person.FirstName),
                                                            new XAttribute("LastName", person.LastName),
                                                            petCollection.Select(pet => new XElement("Pet", pet.Name)
                                                            ))));

            Console.WriteLine();
            Console.WriteLine(ownersAndPets);
        }
    }
}
