using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{

   public class Book
    {
       public static String DownloadBook(string address)
        {
            WebClient client = new WebClient();
            string book = client.DownloadString(address);
            return book; // as a string.
        }
        public static void countWords(String book, String xmlName) // counts the words in the book and write them into a xml file.
        {

            String[] Value = book.Split(' ');
            
            Dictionary<string, int> RepeatedWordCount = new Dictionary<string, int>();
            for (int i = 0; i < Value.Length; i++) //loop the splited string  
            {
                if (RepeatedWordCount.ContainsKey(Value[i].ToLower().Trim())) // Check if word already exist in dictionary update the count  
                {
                    int value = RepeatedWordCount[Value[i].ToLower().Trim()];
                    RepeatedWordCount[Value[i].ToLower().Trim()] = value + 1;
                }
                else
                {
                    RepeatedWordCount.Add(Value[i].ToLower().Trim(), 1);  // if a string is repeated and not added in dictionary , here we are adding   
                }
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);// in order to save the xml onto desktop.
           

            XmlWriter xmlWriter = XmlWriter.Create(desktopPath+"\\"+xmlName + ".xml");

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("words");

            foreach (KeyValuePair<string, int> kvp in RepeatedWordCount)
            {
                xmlWriter.WriteStartElement("word");
                String word = kvp.Key;
                String countOfWord = kvp.Value.ToString();
                xmlWriter.WriteAttributeString("text", word);
                xmlWriter.WriteAttributeString("count", countOfWord);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }

    }
   public class Program
    {
        public static void Main(string[] args)
        {
            String book=Book.DownloadBook("http://www.gutenberg.org/files/2701/2701-0.txt"); // the book is assigned to String book variable.
            Book.countWords(book, "MobyDick"); // it is instructed to be saved into the book.xml.
        

        }
    }
   
}
