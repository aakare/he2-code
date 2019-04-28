using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HePlug;

namespace He2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            System.IO.Directory.CreateDirectory(@"C:\He2");
            
            List<Plugin> pList = new List<Plugin>();

            string[] plugins = Directory.GetFiles("C:\He2", "*.dll");
            foreach (string plugin in plugins)
            {
                if (!plugin.EndsWith("HePlug.dll"))
                {
                    Assembly.LoadFrom(plugin);
                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (Type t in a.GetTypes())
                        {
                            if (t.GetInterface("Plugin") != null)
                            {
                                Plugin p = Activator.CreateInstance(t) as Plugin;
                                pList.Add(p);
                            }
                        }
                    }
                }
            }
            bool operate = true;
            Console.Title = "He2";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            while (operate)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("~" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("$ ");
                Console.ForegroundColor = ConsoleColor.White;
                string i;
                i = Console.ReadLine();
                string[] il = i.Split(' ');
                Console.ForegroundColor = ConsoleColor.Yellow;
                bool pluginExed = false;
                foreach (Plugin plugin in pList)
                {
                    if (sCommand[0].ToLower() == plugin.getCommand().ToLower())
                    {
                        plugin.run(sCommand.Skip(1).ToArray());
                        pluginExed = true;
                        break;
                    }
                }
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
                        if (arg == "--list")
                        {
                            Console.WriteLine("HePlug | Plugin list");
                        }
                        if (arg == "?" || arg == "h")
                        {
                            Console.WriteLine("HePlug | help");
                            Console.WriteLine("? / (empty) - help");
                            Console.WriteLine("--list - list all plugins");
                        }
                    }

                else
                    if (!pluginExed)
                        Console.WriteLine("~if: command not found");
            }
        }
    }
}
