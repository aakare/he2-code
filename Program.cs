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
                i = i.Split();
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (i[0] == "cnm")
                    Console.WriteLine(Dns.GetHostName());
                
                else if (i[0] == "ipls")
                    foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                    {
                        Console.WriteLine(ip);
                    }
                else if (i[0] == "exit")
                    operate = false;
                else
                    Console.WriteLine("~cline: command not found");
            }
        }
    }
}
    

