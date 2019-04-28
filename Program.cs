using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace He2
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Directory.CreateDirectory(@"C:\He2");
            string[] files = System.IO.Directory.GetFiles(@"C:\He2", "*.dll");
            bool operate = true;
            Console.Title = "He2";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(@"
██╗  ██╗███████╗██████╗ 
██║  ██║██╔════╝╚════██╗
███████║█████╗   █████╔╝
██╔══██║██╔══╝  ██╔═══╝ 
██║  ██║███████╗███████╗
╚═╝  ╚═╝╚══════╝╚══════╝
                        ");
            while (operate)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("~" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(" >> ");
                Console.ForegroundColor = ConsoleColor.White;
                string i;
                i = Console.ReadLine();
                string[] il = i.Split(' ');
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (il[0] == "cnm")
                    Console.WriteLine(Dns.GetHostName());
                else if (il[0] == "put")
                    Console.WriteLine(i.Substring(4));
                else if (il[0] == "ipls")
                    foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                        Console.WriteLine(ip);
                else if (il[0] == "exit")
                    operate = false;
                else if (il[0] == "clt")
                    Console.Clear();
                else if (il[0] == "p")
                    foreach (string arg in il)
                    {
                        if(arg == "--list")
                        {
                            Console.WriteLine("HPlug 1.3 | Plugin list");
                            foreach (string file in files)
                                Console.WriteLine(file.Substring(7));
                        }
                        if (arg == "?" || arg == "h")
                        {
                            Console.WriteLine("HPlug 1.3 | help");
                            Console.WriteLine("? / (empty) - help");
                            Console.WriteLine("--list - list all plugins");
                        }
                    }
                        
                else
                    Console.WriteLine("~cline: command not found");
            }
        }
    }
}
