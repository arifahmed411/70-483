using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDemo
{
    class DataContractJsonSerializerDemo
    {

        public static void RunDataContractJsonSerializerDemo()
        {
            Person p = new Person("Arif", "Ahmed", 1);
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            ser.WriteObject(stream1, p);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Console.Write("JSON form of Person object: ");
            Console.WriteLine(sr.ReadToEnd());

            stream1.Position = 0;
            Person p2 = (Person)ser.ReadObject(stream1);
            Console.Write("Deserialized back, got name=");
            Console.Write(p2.FirstName + " " + p2.LastName);
            Console.Write(", id=");
            Console.WriteLine(p2.ID);

        }
    }
}
