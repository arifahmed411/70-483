using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SerializationDemo
{
    class JavaScriptSerializerDemo
    {
        public static void RunJavaScriptSerializationDemo()
        {
            var RegisteredUsers = new List<People>();
            RegisteredUsers.Add(new People() {FirstName = "Farah", LastName = "Ahmed", ID = 1 });
            RegisteredUsers.Add(new People() {FirstName = "Shaekh", LastName = "Ahmed", ID = 2 });
            RegisteredUsers.Add(new People() {FirstName = "Saif", LastName = "Ahmed", ID = 3 });
            RegisteredUsers.Add(new People() { FirstName = "Arif", LastName = "Ahmed", ID = 4 });

            //Cannot serialize object that has no parameterless constructor
            //Doesn't require the Serializable attribute
            //Does not require type in Serializer constructor
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(RegisteredUsers);
            // Produces string value of:
            // [
            //     {"PersonID":1,"Name":"Bryon Hetrick","Registered":true},
            //     {"PersonID":2,"Name":"Nicole Wilcox","Registered":true},
            //     {"PersonID":3,"Name":"Adrian Martinson","Registered":false},
            //     {"PersonID":4,"Name":"Nora Osborn","Registered":false}
            // ]

            var deserializedResult = serializer.Deserialize<List<People>>(serializedResult);
            // Produces List with 4 Person objects

            deserializedResult.ForEach(p => Console.WriteLine($"{p.FirstName}, {p.LastName}, {p.ID}"));
        }

    }
}
