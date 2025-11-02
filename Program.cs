using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lesson7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string FILENAME = "C:\\Users\\Yeshiva\\Desktop\\C#\\י''ד\\Lesson2\\Lesson22\\lesson7\\lesson7\\pelephone.xml";
            XDocument doc = XDocument.Load(FILENAME);
            phone p = new phone();
            p.addCall(doc);
            doc.Save(FILENAME);
            var longCalls = p.getLongCalls(doc, "1");
            foreach (var call in longCalls)
            {
                Console.WriteLine($"Start: {call.Attribute("start")?.Value}, Duration: {call.Attribute("duration")?.Value}, Destination: {call.Attribute("destination")?.Value}");
            }

            //קריאה לבדוק אם זה מקולקל:
            if (p.isPhoneBroken(doc, "2"))
            {
                Console.WriteLine("הטלפון מתקלקל!");
            }
            



        }
    }
}
