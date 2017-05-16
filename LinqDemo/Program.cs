using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqDemo
{
    class LinqExamples
    {
        static void Main(string[] args)
        {
            RunManager.Runner.RunAllExamples(new LinqExamples());
        }

        public static void ExampleAggregate()
        {

            string sentence = "the quick brown fox jumps over the lazy dog";

            // Split the string into individual words.
            string[] words = sentence.Split(' ');

            // Prepend each word to the beginning of the 
            // new sentence to reverse the word order.
            string reversed = words.Aggregate((workingSentence, next) =>
                                                  next + " " + workingSentence);

            Console.WriteLine(reversed);

            // This code produces the following output:
            //
            // dog lazy the over jumps fox brown quick the 

            Console.WriteLine();
            Console.WriteLine();

            int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };

            // Count the even numbers in the array, using a seed value of 0.
            int numEven = ints.Aggregate(0, (total, next) =>
                                                next % 2 == 0 ? total + 1 : total);

            Console.WriteLine("The number of even integers is: {0}", numEven);

            // This code produces the following output:
            //
            // The number of even integers is: 6 

            Console.WriteLine();
            Console.WriteLine();

            string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };

            // Determine whether any string in the array is longer than "banana".
            string longestName =
                fruits.Aggregate("banana",
                                (longest, next) =>
                                    next.Length > longest.Length ? next : longest,
                                // Return the final result as an upper case string.
                                fruit => fruit.ToUpper());

            Console.WriteLine(
                "The fruit with the longest name is {0}.",
                longestName);

            // This code produces the following output:
            //
            // The fruit with the longest name is PASSIONFRUIT. 

        }

        public static void ExampleCustomJoin()
        {
            CustomJoins app = new CustomJoins();
            app.CrossJoin();
            app.NonEquijoin();
        }

        public static void ExampleGroupJoin()
        {
            GroupJoin.ExampleGroupJoin();            
        }

        public static void ExampleLeftOuterJoin()
        {
            LeftOuterJoin.LeftOuterJoinExample();
        }

        public static void ExampleOrderByJoin()
        {
            OrderByJoin o = new OrderByJoin();
            o.OrderJoin();
        }

        public static void ExampleGroupByBool()
        {
            GroupByDemo.ExampleGroupByBool();
        }

        public static void ExampleGroupByNumericRange()
        {
            GroupByDemo.ExampleGroupByNumericRange();
        }

        public static void ExampleGroupByInto()
        {
            GroupByDemo.ExampleGroupByInto();
        }

        public static void GroupByExample1()
        {
            GroupByDemo.GroupByExample1();
        }

        public static void GroupByExample2()
        {
            GroupByDemo.GroupByExample2();
        }

        public static void GroupByExample3()
        {
            GroupByDemo.GroupByExample3();
        }

        public static void ExampleSelectMany()
        {
            new SelectManyDemo().ExampleSelectMany();
        }

        public static void ExampleSelectMany2()
        {
            new SelectManyDemo().ExampleSelectMany2();
        }

        public static void ExampleSelectMany3()
        {
            new SelectManyDemo().ExampleSelectMany3();
        }
    }
}
