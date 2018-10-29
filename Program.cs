using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using static ConvertJsonEventsOfCalendarToIcsFile.Model;

namespace ConvertJsonEventsOfCalendarToIcsFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string distantJsonUrl = "http://srv-ipso.cloudapp.net/getevents";
            try
            {
                var json = new WebClient().DownloadString(distantJsonUrl);
                var listEvents = JsonConvert.DeserializeObject<List<Event>>(json);
                 StringBuilder sb = WriteICallFormat(listEvents);
                string path = AppDomain.CurrentDomain.BaseDirectory +  "events.ics";
                System.IO.File.WriteAllText(path, sb.ToString());
                Console.WriteLine("icall file succesfully saved at path : " + path);
                Console.WriteLine(" \n please press enter for exit");
            }
            catch (WebException e)
            {
                Console.WriteLine("impossible d'atteindre l'url {0}, server exception msg : {1}", distantJsonUrl, e.Message);
                Console.WriteLine(" \n please press enter for exit");
            }
            catch(Exception e)
            {
                Console.WriteLine("fatal error : ",e.Message);
                 Console.WriteLine(" \n please press enter for exit");
            }   
            Console.ReadLine();
        }

        static StringBuilder WriteICallFormat(List<Event> events)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Fgodin//test");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");

            foreach (Event e in events)
            {
                e.Add(sb);
            }
            sb.AppendLine("END:VCALENDAR");
            return sb;
        }
    }
}
