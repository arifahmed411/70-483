using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    class GroupByDemo
    {
        // The element type of the data source.
        public class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
        }

        public static List<Student> GetStudents()
        {
            // Use a collection initializer to create the data source. Note that each element
            //  in the list contains an inner sequence of scores.
            List<Student> students = new List<Student>
            {
               new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
               new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
               new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
               new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
               new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };

            return students;
        }


        public static void ExampleGroupByBool()
        {
            // Obtain the data source.
            List<Student> students = GetStudents();

            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var booleanGroupQuery =
                from student in students
                group student by student.Scores.Average() >= 80; //pass or fail!

            //booleanGroupQuery = students.GroupBy(student => student.Scores.Average() >= 80);

            // Execute the query and access items in each group
            foreach (var studentGroup in booleanGroupQuery)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }

            /* Output:
              Low averages
               Omelchenko, Svetlana:77.5
               O'Donnell, Claire:72.25
               Garcia, Cesar:75.5
              High averages
               Mortensen, Sven:93.5
               Garcia, Debra:88.25
            */
        }

        public static void ExampleGroupByNumericRange()
        {
            // Obtain the data source.
            List<Student> students = GetStudents();

            // Write the query.
            var studentQuery =
                from student in students
                let avg = (int)student.Scores.Average()
                group student by (avg == 0 ? 0 : avg / 10) into g
                orderby g.Key
                select g;

            //studentQuery = students.GroupBy(student =>
            //                {
            //                    int avg = (int)student.Scores.Average();
            //                    if (avg == 0)
            //                        return 0;
            //                    else
            //                        return avg / 10;
            //                }).OrderBy(g => g.Key);

            // Execute the query.
            foreach (var studentGroup in studentQuery)
            {
                int temp = studentGroup.Key * 10;
                Console.WriteLine("Students with an average between {0} and {1}", temp, temp + 10);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }

            /* Output:
                 Students with an average between 70 and 80
                   Omelchenko, Svetlana:77.5
                   O'Donnell, Claire:72.25
                   Garcia, Cesar:75.5
                 Students with an average between 80 and 90
                   Garcia, Debra:88.25
                 Students with an average between 90 and 100
                   Mortensen, Sven:93.5
             */
        }

        public static void ExampleGroupByInto()
        {
            // Create the data source.
            string[] words2 = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese", "elephant", "umbrella", "anteater" };

            // Create the query.
            var wordGroups2 =
                from w in words2
                group w by w[0] into grps
                where (grps.Key == 'a' || grps.Key == 'e' || grps.Key == 'i'
                       || grps.Key == 'o' || grps.Key == 'u')
                select grps;

            //wordGroups2 = words2.GroupBy(w => w[0]).Where(grps => 
            //                                    grps.Key == 'a' || grps.Key == 'e' || grps.Key == 'i'
            //                                               || grps.Key == 'o' || grps.Key == 'u');

            // Execute the query.
            foreach (var wordGroup in wordGroups2)
            {
                Console.WriteLine("Groups that start with a vowel: {0}", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine("   {0}", word);
                }
            }

            /* Output:
                Groups that start with a vowel: a
                    abacus
                    apple
                    anteater
                Groups that start with a vowel: e
                    elephant
                Groups that start with a vowel: u
                    umbrella
            */
        }

        class Pet
        {
            public string Name { get; set; }
            public double Age { get; set; }
        }

        // Uses method-based query syntax.
        public static void GroupByExample1()
        {
            // Create a list of pets.
            List<Pet> pets =
                new List<Pet>{ new Pet { Name="Barley", Age=8 },
                       new Pet { Name="Boots", Age=4 },
                       new Pet { Name="Whiskers", Age=1 },
                       new Pet { Name="Daisy", Age=4 } };

            // Group the pets using Age as the key value 
            // and selecting only the pet's Name for each value.
            IEnumerable<IGrouping<double, string>> query =
                pets.GroupBy(pet => pet.Age, pet => pet.Name);

            // Iterate over each IGrouping in the collection.
            foreach (IGrouping<double, string> petGroup in query)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(petGroup.Key);
                // Iterate over each value in the 
                // IGrouping and print the value.
                foreach (string name in petGroup)
                    Console.WriteLine("  {0}", name);
            }

            /*
             This code produces the following output:

             8
               Barley
             4
               Boots
               Daisy
             1
               Whiskers
            */
        }

        public static void GroupByExample2()
        {
            // Create a list of pets.
            List<Pet> petsList =
                new List<Pet>{ new Pet { Name="Barley", Age=8.3 },
                       new Pet { Name="Boots", Age=4.9 },
                       new Pet { Name="Whiskers", Age=1.5 },
                       new Pet { Name="Daisy", Age=4.3 } };

            // Group Pet objects by the Math.Floor of their age.
            // Then project an anonymous type from each group
            // that consists of the key, the count of the group's
            // elements, and the minimum and maximum age in the group.
            var query = petsList.GroupBy(
                pet => Math.Floor(pet.Age),
                (age, pets) => new
                {
                    Key = age,
                    Count = pets.Count(),
                    Min = pets.Min(pet => pet.Age),
                    Max = pets.Max(pet => pet.Age)
                });

            // Iterate over each anonymous type.
            foreach (var result in query)
            {
                Console.WriteLine("\nAge group: " + result.Key);
                Console.WriteLine("Number of pets in this age group: " + result.Count);
                Console.WriteLine("Minimum age: " + result.Min);
                Console.WriteLine("Maximum age: " + result.Max);
            }

            /*  This code produces the following output:

                Age group: 8
                Number of pets in this age group: 1
                Minimum age: 8.3
                Maximum age: 8.3

                Age group: 4
                Number of pets in this age group: 2
                Minimum age: 4.3
                Maximum age: 4.9

                Age group: 1
                Number of pets in this age group: 1
                Minimum age: 1.5
                Maximum age: 1.5
            */
        }

        public static void GroupByExample3()
        {
            // Create a list of pets.
            List<Pet> petsList =
                new List<Pet>{ new Pet { Name="Barley", Age=8.3 },
                       new Pet { Name="Boots", Age=4.9 },
                       new Pet { Name="Whiskers", Age=1.5 },
                       new Pet { Name="Daisy", Age=4.3 } };

            // Group Pet.Age values by the Math.Floor of the age.
            // Then project an anonymous type from each group
            // that consists of the key, the count of the group's
            // elements, and the minimum and maximum age in the group.
            var query = petsList.GroupBy(
                pet => Math.Floor(pet.Age),
                pet => pet.Age,
                (baseAge, ages) => new
                {
                    Key = baseAge,
                    Count = ages.Count(),
                    Min = ages.Min(),
                    Max = ages.Max()
                });

            // Iterate over each anonymous type.
            foreach (var result in query)
            {
                Console.WriteLine("\nAge group: " + result.Key);
                Console.WriteLine("Number of pets in this age group: " + result.Count);
                Console.WriteLine("Minimum age: " + result.Min);
                Console.WriteLine("Maximum age: " + result.Max);
            }

            /*  This code produces the following output:

                Age group: 8
                Number of pets in this age group: 1
                Minimum age: 8.3
                Maximum age: 8.3

                Age group: 4
                Number of pets in this age group: 2
                Minimum age: 4.3
                Maximum age: 4.9

                Age group: 1
                Number of pets in this age group: 1
                Minimum age: 1.5
                Maximum age: 1.5
            */
        }

    }
}
