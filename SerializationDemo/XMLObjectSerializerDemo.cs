using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SerializationDemo
{
    public class XMLObjectSerializerDemo
    {
        private void WriteObjectContentInDocument(XmlObjectSerializer xm, Person p,
           string fileName)
        {

            // Create the writer object.
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlDictionaryWriter writer =
                XmlDictionaryWriter.CreateTextWriter(fs);

            DataContractSerializer ser =
                new DataContractSerializer(typeof(Person));

            // Use the writer to start a document.
            writer.WriteStartDocument(true);

            //// Use the writer to write the root element.
            //writer.WriteStartElement("Company");

            //// Use the writer to write an element.
            //writer.WriteElementString("Name", "Microsoft");

            // Use the serializer to write the start,
            // content, and end data.
            xm.WriteStartObject(writer, p);
            xm.WriteObjectContent(writer, p);
            xm.WriteEndObject(writer);

            // Use the writer to write the end element and
            // the end of the document.
            writer.WriteEndElement();
            writer.WriteEndDocument();

            // Close and release the writer resources.
            writer.Flush();
            fs.Flush();
            fs.Close();
        }


        private void WriteObjectWithInstance(XmlObjectSerializer xm, Person p,
           string fileName)
        {
            // Use either the XmlDataContractSerializer or NetDataContractSerializer,
            // or any other class that inherits from XmlObjectSerializer to write with.
            Console.WriteLine(xm.GetType());
            using (Stream fs = new FileStream(fileName, FileMode.Create))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    //This does not work
                    //xm.WriteObject(writer, p);

                    //This works
                    ////xm.WriteObject(fs, p);

                    // Use the writer to start a document.
                    writer.WriteStartDocument(true);

                    // Use the serializer to write the start,
                    // content, and end data.
                    xm.WriteStartObject(writer, p);
                    xm.WriteObjectContent(writer, p);
                    xm.WriteEndObject(writer);

                    writer.WriteEndDocument();

                    Console.WriteLine("Done writing {0}", fileName);
                }
            };

        }

        public static void RunXMLObjectSerializerDemo()
        {
            try
            {
                Console.WriteLine("Starting");
                XMLObjectSerializerDemo t = new XMLObjectSerializerDemo();

                // Create the object to write to a file.
                Person p1 = new Person() { FirstName = "Zighetti", LastName = "Barbara", ID = 101 };

                // Create a DataContractSerializer and a NetDataContractSerializer.
                // Pass either one to the WriteObjectWithInstance method.
                DataContractSerializer dcs = new DataContractSerializer(typeof(Person));
                NetDataContractSerializer ndcs = new NetDataContractSerializer();
                t.WriteObjectWithInstance(dcs, p1, @"datacontract.xml");
                t.WriteObjectWithInstance(ndcs, p1, @"netDatacontract.xml");

                //WriteObjectContentInDocument(@"datacontract.xml");

                Console.WriteLine("Done");
                //Console.ReadLine();
            }

            catch (InvalidDataContractException iExc)
            {
                Console.WriteLine("You have an invalid data contract: ");
                Console.WriteLine(iExc.Message);
                Console.ReadLine();
            }

            catch (SerializationException serExc)
            {
                Console.WriteLine("There is a problem with the instance:");
                Console.WriteLine(serExc.Message);
                Console.ReadLine();
            }

            catch (QuotaExceededException qExc)
            {
                Console.WriteLine("The quota has been exceeded");
                Console.WriteLine(qExc.Message);
                Console.ReadLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine(exc.ToString());
                Console.ReadLine();
            }
        }
    }
}
