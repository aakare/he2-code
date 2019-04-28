using System;
using System.Collections.Generic;
using System.IO;
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
        static bool operate = true;

        static List<Plugin> pList = new List<Plugin>();
        static void Main(string[] args)
        {

            System.IO.Directory.CreateDirectory(@"C:\He2");

            string[] plugins = System.IO.Directory.GetFiles(@"C:\He2", "*.dll");
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
            Console.Title = "He2";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine();
            if (args.Length > 0)
            {
                StreamReader sr = new StreamReader(args[0]);
                string[] commands = sr.ReadToEnd().Split('\n');

                foreach (string command in commands)
                {
                    if(command.Length > 0)
                        execCommand(command);
                }
            }
            else
                while (operate)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("~" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("$ ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string i;
                    i = Console.ReadLine();
                    execCommand(i);
                }
        }

        public static void execCommand(string i)
        {
            string[] il = i.Split(' ');
            Console.ForegroundColor = ConsoleColor.Yellow;
            bool pluginExed = false;
            foreach (Plugin plugin in pList)
            {
                if (il[0].ToLower() == "./" + plugin.Name().ToLower())
                {
                    plugin.Code(il.Skip(1).ToArray());
                    pluginExed = true;
                    break;
                }
            }
            if (il[0] == "stop")
                operate = false;
            else if (il[0] == "clt")
                Console.Clear();
            else if (il[0] == "##end")
                Console.ReadKey();
            else if (il[0] == "?" || il[0] == "wonders")
                if (il[1] == "")
                {
                    Console.WriteLine("He2 Wonders");
                    Console.WriteLine("? - help | arguments: --l - plugin list");
                    Console.WriteLine("");
                }
                else
                    foreach (string arg in il)
                    {
                        if (arg == "--l")
                        {
                            Console.WriteLine("HePlug | Plugin list");
                            Console.WriteLine();
                            foreach (Plugin plugin in pList)
                            {
                                Console.WriteLine("Name > " + plugin.Name() + " > Description > " + plugin.Description() + " > Help > " + plugin.Help());
                            }
                        }
                    }

            else
                if (!pluginExed)
                Console.WriteLine("~if: command not found, \"?\" for help.");
        }
        }
}
