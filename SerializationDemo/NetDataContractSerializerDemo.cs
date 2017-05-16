using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SerializationDemo
{
    public sealed class NetDataContractSerializerDemo
    {
        public static void RunNetDataContractSerializerDemo()
        {
            try
            {
                WriteObject("NetDataContractSerializerExample.xml");
                ReadObject("NetDataContractSerializerExample.xml");
            }

            catch (SerializationException serExc)
            {
                Console.WriteLine("Serialization Failed");
                Console.WriteLine(serExc.Message);
            }
            catch (Exception exc)
            {
                Console.WriteLine(
                "The serialization operation failed: {0} StackTrace: {1}",
                exc.Message, exc.StackTrace);
            }

            finally
            {
                //Console.WriteLine("Press <Enter> to exit....");
                //Console.ReadLine();
            }
        }

        public static void WriteObject(string fileName)
        {
            Console.WriteLine(
                "Creating a Person object and serializing it.");
            Person p1 = new Person("Zighetti", "Barbara", 101);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
            NetDataContractSerializer ser =
                new NetDataContractSerializer();
            ser.WriteObject(writer, p1);
            writer.Close();
        }

        public static void ReadObject(string fileName)
        {
            Console.WriteLine("Deserializing an instance of the object.");
            FileStream fs = new FileStream(fileName,
            FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            NetDataContractSerializer ser = new NetDataContractSerializer();

            // Deserialize the data and read it from the instance.
            Person deserializedPerson =
                (Person)ser.ReadObject(reader, true);
            fs.Close();
            Console.WriteLine(String.Format("{0} {1}, ID: {2}",
            deserializedPerson.FirstName, deserializedPerson.LastName,
            deserializedPerson.ID));
        }
    }
}
