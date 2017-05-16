using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    class PetOwner
    {
        public string Name { get; set; }
        public List<String> Pets { get; set; }
    }

    class SelectManyDemo
    {
        public void ExampleSelectMany()
        {
        PetOwner[] petOwners =
                { new PetOwner { Name="Higa, Sidney",
                      Pets = new List<string>{ "Scruffy", "Sam" } },
                  new PetOwner { Name="Ashkenazi, Ronen",
                      Pets = new List<string>{ "Walker", "Sugar" } },
                  new PetOwner { Name="Price, Vernette",
                      Pets = new List<string>{ "Scratches", "Diesel" } } };

        // Query using SelectMany().
        IEnumerable<string> query1 = petOwners.SelectMany(petOwner => petOwner.Pets);

            Console.WriteLine("Using SelectMany():");

            // Only one foreach loop is required to iterate 
            // through the results since it is a
            // one-dimensional collection.
            foreach (string pet in query1)
            {
                Console.WriteLine(pet);
            }


            // This code shows how to use Select() 
            // instead of SelectMany().
            IEnumerable<List<String>> query2 =
                petOwners.Select(petOwner => petOwner.Pets);

            Console.WriteLine("\nUsing Select():");

            // Notice that two foreach loops are required to 
            // iterate through the results
            // because the query returns a collection of arrays.
            foreach (List<String> petList in query2)
            {
                foreach (string pet in petList)
                {
                    Console.WriteLine(pet);
                }
                Console.WriteLine();
            }

            /*
             This code produces the following output:

             Using SelectMany():
             Scruffy
             Sam
             Walker
             Sugar
             Scratches
             Diesel

             Using Select():
             Scruffy
             Sam

             Walker
             Sugar

             Scratches
             Diesel
            */
        }

        public void ExampleSelectMany2()
        {
            PetOwner[] petOwners =
                    { new PetOwner { Name="Higa, Sidney",
                          Pets = new List<string>{ "Scruffy", "Sam" } },
                      new PetOwner { Name="Ashkenazi, Ronen",
                          Pets = new List<string>{ "Walker", "Sugar" } },
                      new PetOwner { Name="Price, Vernette",
                          Pets = new List<string>{ "Scratches", "Diesel" } },
                      new PetOwner { Name="Hines, Patrick",
                          Pets = new List<string>{ "Dusty" } } };

            // Project the items in the array by appending the index 
            // of each PetOwner to each pet's name in that petOwner's 
            // array of pets.
            IEnumerable<string> query =
                petOwners.SelectMany((petOwner, index) =>
                                         petOwner.Pets.Select(pet => index + pet));

            foreach (string pet in query)
            {
                Console.WriteLine(pet);
            }

            // This code produces the following output:
            //
            // 0Scruffy
            // 0Sam
            // 1Walker
            // 1Sugar
            // 2Scratches
            // 2Diesel
            // 3Dusty
        }

        public void ExampleSelectMany3()
        {
            PetOwner[] petOwners =
                            { new PetOwner { Name="Higa",
                                      Pets = new List<string>{ "Scruffy", "Sam" } },
                                  new PetOwner { Name="Ashkenazi",
                                      Pets = new List<string>{ "Walker", "Sugar" } },
                                  new PetOwner { Name="Price",
                                      Pets = new List<string>{ "Scratches", "Diesel" } },
                                  new PetOwner { Name="Hines",
                                      Pets = new List<string>{ "Dusty" } } };


            // Project the pet owner's name and the pet's name.
            var query =
                petOwners
                .SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName })
                .Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"))
                .Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.petOwner.Name,
                            Pet = ownerAndPet.petName
                        }
                );

            // Print the results.
            foreach (var obj in query)
            {
                Console.WriteLine(obj);
            }

            // This code produces the following output:
            //
            // {Owner=Higa, Pet=Scruffy}
            // {Owner=Higa, Pet=Sam}
            // {Owner=Ashkenazi, Pet=Sugar}
            // {Owner=Price, Pet=Scratches}
        }

    }
}
