using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XMLDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            RunManager.Runner.RunEverything(new Program());
            RunManager.Runner.RunEverything(new XPathValidation());
            RunManager.Runner.RunEverything(new XMLDocumentNavigation());
        }

        public static void SimpleExample()
        {
            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<item><name>wrench</name></item>");

            // Add a price element.
            XmlElement newElem = doc.CreateElement("price");
            newElem.InnerText = "10.95";
            doc.DocumentElement.AppendChild(newElem);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            // Save the document to a file and auto-indent the output.
            using (XmlWriter writer = XmlWriter.Create("data.xml", settings))
                doc.Save(writer);

            Console.WriteLine(doc.OuterXml);
        }

        public static void LoadDocumentWithSchemaValidation()
        {
            Program app = new Program();
            app.LoadDocumentWithSchemaValidation(true, true);
        }

        //************************************************************************************
        //
        //  Associate the schema with XML. Then, load the XML and validate it against
        //  the schema.
        //
        //************************************************************************************
        public XmlDocument LoadDocumentWithSchemaValidation(bool generateXML, bool generateSchema)
        {
            XmlReader reader;

            XmlReaderSettings settings = new XmlReaderSettings();

            // Helper method to retrieve schema.
            XmlSchema schema = getSchema(generateSchema);

            if (schema == null)
            {
                return null;
            }

            settings.Schemas.Add(schema);

            settings.ValidationEventHandler += settings_ValidationEventHandler;
            settings.ValidationFlags =
                settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            try
            {
                reader = XmlReader.Create("data.xml", settings);
            }
            catch (System.IO.FileNotFoundException)
            {
                if (generateXML)
                {
                    string xml = generateXMLString();
                    byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                    MemoryStream stream = new MemoryStream(byteArray);
                    reader = XmlReader.Create(stream, settings);
                }
                else
                {
                    return null;
                }

            }

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(reader);
            reader.Close();

            return doc;
        }


        private static string generateXMLString() =>
                "<?xml version=\"1.0\"?> \n" +
                "<books xmlns=\"http://www.contoso.com/books\"> \n" +
                "  <book genre=\"novel\" ISBN=\"1-861001-57-8\" publicationdate=\"1823-01-28\"> \n" +
                "    <title>Pride And Prejudice</title> \n" +
                "    <price>24.95</price> \n" +
                "  </book> \n" +
                "  <book genre=\"novel\" ISBN=\"1-861002-30-1\" publicationdate=\"1985-01-01\"> \n" +
                "    <title>The Handmaid's Tale</title> \n" +
                "    <price>29.95</price> \n" +
                "  </book> \n" +
                "</books>";

        public static XmlDocument LoadXMLtoDOM()
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try {
                doc.Load("data.xml");
            }
            catch (System.IO.FileNotFoundException)
            {
                doc.LoadXml(generateXMLString());
            }

            return doc;
        }

        //************************************************************************************
        //
        //  Helper method that generates an XML Schema.
        //
        //************************************************************************************
        private string generateXMLSchema()
        {
            string xmlSchema =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?> " +
                "<xs:schema attributeFormDefault=\"unqualified\" " +
                "elementFormDefault=\"qualified\" targetNamespace=\"http://www.contoso.com/books\" " +
                "xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"> " +
                "<xs:element name=\"books\"> " +
                "<xs:complexType> " +
                "<xs:sequence> " +
                "<xs:element maxOccurs=\"unbounded\" name=\"book\"> " +
                "<xs:complexType> " +
                "<xs:sequence> " +
                "<xs:element name=\"title\" type=\"xs:string\" /> " +
                "<xs:element name=\"price\" type=\"xs:decimal\" /> " +
                "</xs:sequence> " +
                "<xs:attribute name=\"genre\" type=\"xs:string\" use=\"required\" /> " +
                "<xs:attribute name=\"publicationdate\" type=\"xs:date\" use=\"required\" /> " +
                "<xs:attribute name=\"ISBN\" type=\"xs:string\" use=\"required\" /> " +
                "</xs:complexType> " +
                "</xs:element> " +
                "</xs:sequence> " +
                "</xs:complexType> " +
                "</xs:element> " +
                "</xs:schema> ";
            return xmlSchema;
        }

        //************************************************************************************
        //
        //  Helper method that gets a schema
        //
        //************************************************************************************
        private XmlSchema getSchema(bool generateSchema)
        {
            XmlSchemaSet xs = new XmlSchemaSet();
            XmlSchema schema;
            try
            {
                schema = xs.Add("http://www.contoso.com/books", "booksData.xsd");
            }
            catch (System.IO.FileNotFoundException)
            {
                if (generateSchema)
                {
                    string xmlSchemaString = generateXMLSchema();
                    byte[] byteArray = Encoding.UTF8.GetBytes(xmlSchemaString);
                    MemoryStream stream = new MemoryStream(byteArray);
                    XmlReader reader = XmlReader.Create(stream);
                    schema = xs.Add("http://www.contoso.com/books", reader);
                }
                else
                {
                    return null;
                }

            }
            return schema;
        }

        //************************************************************************************
        //
        //  Event handler that is raised when XML doesn't validate against the schema.
        //
        //************************************************************************************
        void settings_ValidationEventHandler(object sender,
            System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.WriteLine
                    ("The following validation warning occurred: " + e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.WriteLine
                    ("The following critical validation errors occurred: " + e.Message);
                Type objectType = sender.GetType();
            }

        }


    }
}
