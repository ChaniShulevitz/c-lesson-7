using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lesson7
{
    public class phone
    {

        //מקבל מהמשתמש מספר מכשיר ומחזיר את כל השיחות הארוכות שהתבצעו מהמכשיר הזה
        public List<XElement> getLongCalls(XDocument doc, string num)
        {
            var phone = doc.Root.Elements().Where(e => e.Attribute("num").Value == num).FirstOrDefault();
            if (phone == null)
                return new List<XElement>();
            var longCalls = phone.Element("calls").Elements("call")
                                        .Where(e => int.Parse(e.Attribute("duration").Value) >= 45)
                                        .ToList();
            return longCalls;
        }
        // מקבל מהמשתמש מספר טלפון ומחזיר את כל הטלפונים שעשו שיחה עם המספר הזה
        public List<XElement> getphones(XDocument doc, string dest)
        {
            var phones = doc.Root.Elements("phone")
                                      .Where(p => p.Elements("calls")
                                      .Elements("call")
                                       .Any(m => m.Attribute("destination").Value == dest))
                                       .ToList();

            return phones;
        }

        //מחזיר את כל הטלפונים שביצעו שיחה בשעה חמש 
        public List<XElement> getphones2(XDocument doc)
        {
            var phones = doc.Root.Elements("phone")
                                      .Where(p => p.Elements("calls")
                                      .Elements("call")
                                       .Any(m => m.Attribute("start").Value == "05:00"))
                                       .ToList();

            return phones;
        }


        public void addCall(XDocument doc)
        {
            XElement e1 = new XElement("call",
                                        new XAttribute("start", "14:32"),
                                        new XAttribute("duration", "30"),
                                        new XAttribute("destination", "0775404858"));
            doc.Root.Elements("phone")
                .Where(e => e.Attribute("num").Value == "1").First()
                .Element("calls")
                .Add(e1);
            
        }

        // בודקת אם טלפון מקולקל
        public bool isPhoneBroken(XDocument doc, string phoneNum)
        {
            var phone = doc.Root.Elements("phone")
                                .FirstOrDefault(p => p.Attribute("num")?.Value == phoneNum);
            if (phone == null) return true; 

            var calls = phone.Element("calls").Elements("call");
            return !calls.Any(); 
        }



    }
}