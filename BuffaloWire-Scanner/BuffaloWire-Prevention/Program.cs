using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuffaloWire_Prevention
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string p2c = args[0];
            }
            catch
            {
                negative();
                Console.WriteLine(" Missing parameter.");
                Environment.Exit(0);
            }
            string p3c = args[0].ToLower();
            if (p3c == "/f" || p3c == "-f")
            {
                try
                {
                    string p4c = args[1];
                }
                catch
                {
                    negative();
                    Console.WriteLine(" Missing parameter for -f.");
                    Environment.Exit(0);
                }
                positive();
                Console.WriteLine(" Checking...");
                string p5c = args[1];
                if (!File.Exists(p5c))
                {
                    negative();
                    Console.WriteLine(" File does not exist.");
                }
                string p6c = File.ReadAllText(p5c);
                if (p6c.Contains("$MFT"))
                {
                    positive();
                    Console.WriteLine(" File likely DOES have the BuffaloWire exploit embed.");
                    Environment.Exit(0);
                }
                else
                {
                    negative();
                    Console.WriteLine(" File likely DOES NOT have the BuffaloWire exploit embed.");
                    Environment.Exit(0);
                }
            }
            WebClient client = new WebClient();
            try
            {
                string htmlCodee = client.DownloadString(args[0]);
            }
            catch(Exception a)
            {
                negative();
                Console.WriteLine(" There was an error contacting the website's source.");
                negative();
                Console.WriteLine(" Error details: " + a);
                Environment.Exit(0);
            }
            string htmlCode = client.DownloadString(args[0]);
            positive();
            Console.WriteLine(" Checking...");
            if (htmlCode == "")
            {
                negative();
                Console.WriteLine(" Could not find address " + args[0]);
                Environment.Exit(0);
            }
            if (htmlCode.Contains("$MFT"))
            {
                positive();
                Console.WriteLine(" Website " + args[0] + " likely DOES have the BuffaloWire exploit embed.");
                Environment.Exit(0);
            }
            else
            {
                negative();
                Console.WriteLine(" Website " + args[0] + " likely DOES NOT have the BuffaloWire exploit embed.");
                Environment.Exit(0);
            }

            
        }
        public static void negative()
        {
            Console.ForegroundColor = ConsoleColor.Red; Console.Write("[-]"); Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void positive()
        {
            Console.ForegroundColor = ConsoleColor.Green; Console.Write("[+]"); Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void info()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write("[@]"); Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
