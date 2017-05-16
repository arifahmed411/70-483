using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    class SampleAttributeDemo
    {

        public static void runDemo()
        {
            {
                var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetCustomAttributes<SampleAttribute>().Count() > 0
                            select t;

                foreach (var t in types)
                {
                    Console.WriteLine(t.Name);

                    foreach (var p in t.GetProperties())
                    {
                        Console.WriteLine(p);
                    }
                }
            }

        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
        public class SampleAttribute : Attribute
        {
            public string Name { get; set; }
            public int Version { get; set; }

        }

        [Sample(Name = "John", Version = 1)]
        public class Test
        {
            [Sample]
            public int IntValue { get; set; }

            [Sample]
            public void method()
            {

            }
        }

        public class NoAttribute
        {
            public string Name { get; set; }
        }
    }
}