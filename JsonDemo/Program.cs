using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using RunManager;

namespace JsonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner.RunAllExamples(new Program());
        }

        public static void JavaScriptSerializerExample()
        {
            var RegisteredUsers = new List<Person>();
            RegisteredUsers.Add(new Person() { PersonID = 1, Name = "Bryon Hetrick", Registered = true });
            RegisteredUsers.Add(new Person() { PersonID = 2, Name = "Nicole Wilcox", Registered = true });
            RegisteredUsers.Add(new Person() { PersonID = 3, Name = "Adrian Martinson", Registered = false });
            RegisteredUsers.Add(new Person() { PersonID = 4, Name = "Nora Osborn", Registered = false });

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(RegisteredUsers);
            // Produces string value of:
            // [
            //     {"PersonID":1,"Name":"Bryon Hetrick","Registered":true},
            //     {"PersonID":2,"Name":"Nicole Wilcox","Registered":true},
            //     {"PersonID":3,"Name":"Adrian Martinson","Registered":false},
            //     {"PersonID":4,"Name":"Nora Osborn","Registered":false}
            // ]

            Console.WriteLine(serializedResult);

            var deserializedResult = serializer.Deserialize<List<Person>>(serializedResult);
            // Produces List with 4 Person objects
        }


    }

    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public bool Registered { get; set; }
    }
}
