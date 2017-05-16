using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace XMLDemo
{
    class XMLDocumentNavigation
    {
        public static void ObtainRootNode()
        {
            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version='1.0' ?>" +
                        "<book genre='novel' ISBN='1-861001-57-5'>" +
                        "<title>Pride And Prejudice</title>" +
                        "</book>");

            //Display the document element.
            Console.WriteLine(doc.DocumentElement.OuterXml);
        }

        public static void GetChildNodes()
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book ISBN='1-861001-57-5'>" +
                        "<title>Pride And Prejudice</title>" +
                        "<price>19.95</price>" +
                        "</book>");

            XmlNode root = doc.FirstChild;

            Console.WriteLine(root.OuterXml);

            //Display the contents of the child nodes.
            if (root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    Console.WriteLine(root.ChildNodes[i].InnerText);
                }
            }
        }

        public static void GetLastChild()
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<book ISBN='1-861001-57-5'>" +
                        "<title>Pride And Prejudice</title>" +
                        "<price>19.95</price>" +
                        "</book>");

            XmlNode root = doc.FirstChild;

            Console.WriteLine("Display the price element...");
            Console.WriteLine(root.LastChild.OuterXml);
        }

        public static void NavigateForwardAcrossSiblings()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("contosoBooks.xml");

            XmlNode currNode = doc.DocumentElement.FirstChild;
            Console.WriteLine("First book...");
            Console.WriteLine(currNode.OuterXml);

            XmlNode nextNode = currNode.NextSibling;
            Console.WriteLine("\r\nSecond book...");
            Console.WriteLine(nextNode.OuterXml);

        }

        public static void NavigateBackwardsAcrossSiblings()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("contosoBooks.xml");

            XmlNode lastNode = doc.DocumentElement.LastChild;
            Console.WriteLine("Last book...");
            Console.WriteLine(lastNode.OuterXml);

            XmlNode prevNode = lastNode.PreviousSibling;
            Console.WriteLine("\r\nPrevious book...");
            Console.WriteLine(prevNode.OuterXml);
        }

        public XmlNode GetBook(string uniqueAttribute, XmlDocument doc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@ISBN='" + uniqueAttribute + "']";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            return xmlNode;
        }

        public void GetBookInformation(ref string title, ref string ISBN, ref string publicationDate,
            ref string price, ref string genre, XmlNode book)
        {
            XmlElement bookElement = (XmlElement)book;

            // Get the attributes of a book.        
            XmlAttribute attr = bookElement.GetAttributeNode("ISBN");
            ISBN = attr.InnerXml;

            attr = bookElement.GetAttributeNode("genre");
            genre = attr.InnerXml;

            attr = bookElement.GetAttributeNode("publicationdate");
            publicationDate = attr.InnerXml;

            // Get the values of child elements of a book.
            title = bookElement["title"].InnerText;
            price = bookElement["price"].InnerText;
        }

        public static void ChangeNodeValue()
        {
            //This example selects all books where the author's last name is Franklin, and then changes the price of those books.
            XmlDocument doc = new XmlDocument();
            doc.Load("contosoBooks.xml");

            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;

            nodeList = root.SelectNodes("descendant::book[author/last-name='Franklin']");

            //Change the price on the books.
            foreach (XmlNode book in nodeList)
            {
                book.LastChild.InnerText = "15.95";
            }

            Console.WriteLine("Display the modified XML document....");
            doc.Save(Console.Out);

        }

        public static void getNodeCollectionByName()
        {
            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.Load("contosoBooks.xml");

            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName("title");
            for (int i = 0; i < elemList.Count; i++)
            {
                Console.WriteLine(elemList[i].InnerXml);
            }

        }


        public void editBook(string title, string ISBN, string publicationDate,
    string genre, string price, XmlNode book, bool validateNode, bool generateSchema)
        {

            XmlElement bookElement = (XmlElement)book;

            // Get the attributes of a book.        
            bookElement.SetAttribute("ISBN", ISBN);
            bookElement.SetAttribute("genre", genre);
            bookElement.SetAttribute("publicationdate", publicationDate);

            // Get the values of child elements of a book.
            bookElement["title"].InnerText = title;
            bookElement["price"].InnerText = price;

            //if (validateNode)
            //{
            //    validateXML(generateSchema, bookElement.OwnerDocument);
            //}
        }

        public XmlElement AddNewBook(string genre, string ISBN, string misc,
    string title, string price, XmlDocument doc)
        {
            // This example creates a book node, adds attrubutes to that node, and then adds that node to the document.
            // Create a new book element.
            XmlElement bookElement = doc.CreateElement("book", "http://www.contoso.com/books");

            // Create attributes for book and append them to the book element.
            XmlAttribute attribute = doc.CreateAttribute("genre");
            attribute.Value = genre;
            bookElement.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("ISBN");
            attribute.Value = ISBN;
            bookElement.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("publicationdate");
            attribute.Value = misc;
            bookElement.Attributes.Append(attribute);

            // Create and append a child element for the title of the book.
            XmlElement titleElement = doc.CreateElement("title");
            titleElement.InnerText = title;
            bookElement.AppendChild(titleElement);

            // Introduce a newline character so that XML is nicely formatted.
            bookElement.InnerXml =
                bookElement.InnerXml.Replace(titleElement.OuterXml,
                "\n    " + titleElement.OuterXml + " \n    ");

            // Create and append a child element for the price of the book.
            XmlElement priceElement = doc.CreateElement("price");
            priceElement.InnerText = price;
            bookElement.AppendChild(priceElement);

            // Introduce a newline character so that XML is nicely formatted.
            bookElement.InnerXml =
                bookElement.InnerXml.Replace(priceElement.OuterXml, priceElement.OuterXml + "   \n  ");

            return bookElement;

        }

        public void deleteBook(XmlNode book)
        {
            XmlNode prevNode = book.PreviousSibling;

            book.OwnerDocument.DocumentElement.RemoveChild(book);


            if (prevNode.NodeType == XmlNodeType.Whitespace ||
                prevNode.NodeType == XmlNodeType.SignificantWhitespace)
            {
                prevNode.OwnerDocument.DocumentElement.RemoveChild(prevNode);
            }
        }

    }
}
