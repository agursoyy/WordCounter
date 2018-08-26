using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public Word[] readXML()  // this reads the xml created before and returns "Word array" containing words that have maximum counts.
        {
            Word[] words = new Word[10];
            int maxCount = 0;
            String word = "";
            int maximum = 0;
            bool flag = true;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //to access "MobyDick.xml" on desktop.
            XmlReader xmlReader = XmlReader.Create(desktopPath + "\\MobyDick.xml");
            for (int i=0; i<10; i++)
            {

                while (xmlReader.Read())
                {
                    if ((xmlReader.NodeType == XmlNodeType.Element))
                    {
                        if (xmlReader.HasAttributes)
                        {
                            if (Convert.ToInt32(xmlReader.GetAttribute("count")) > maxCount)
                            {
                                if (flag) { 
                                maxCount = Convert.ToInt32(xmlReader.GetAttribute("count"));
                                word = xmlReader.GetAttribute("text");
                                }
                                else if(Convert.ToInt32(xmlReader.GetAttribute("count"))<maximum)
                                {
                                    maxCount = Convert.ToInt32(xmlReader.GetAttribute("count"));
                                    word = xmlReader.GetAttribute("text");
                                }
                            }
                        }
                    }
                   
                }
                desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); //to access "MobyDick.xml" on desktop.
                xmlReader = XmlReader.Create(desktopPath+"\\MobyDick.xml");
                flag = false;
                Word newWord = new Word(word, maxCount);
                words[i] = newWord;
                maximum = maxCount;
                maxCount = 0;
            }
            return words;

        }


        public IActionResult Index()
        {
            Word[] arr = readXML();
            ViewData["words"] = arr;
            return View();
        }

       
    }
}
