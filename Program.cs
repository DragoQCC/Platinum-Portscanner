using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;
using Platinum_portscanner.Commands;

namespace Platinum_portscanner
{
    public class Program
    {
        static void Main(string[] args)
        {
            UI.ui.banner();
            Console.WriteLine("Example required command is tcpscan /hosts ip /ports ports");
            Console.WriteLine("Optional arguments are /nocheck yes skips hosts up check.");
            Console.WriteLine("ex: tcpscan /hosts 127.0.0.1 192.168.1.1 /ports 80 22 8080 53");
            Console.WriteLine("use help command too see all options like cidr notation on hosts, ranges on ports and top ports");
            Console.WriteLine("Enter command");
            string input;

            while (true)
            {
                Console.Write("[PlatPort]>");
                input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                if (input.ToLower() == "help")
                {
                    help.Print();
                }

                if (!ArgParser.checkInput(input))
                {
                    continue;
                }

                TcpScan scanner = new TcpScan();
                scanner.init();
                //clears out the lists and dictionaries before each scan
                CommandBase.Hosts.Clear();
                CommandBase.Ports.Clear();
                CommandBase.AwakeHosts.Clear();
                CommandBase.OpenPorts.Clear();
                CommandBase.ConnctionDictionary.Clear();
                CommandBase.HostOSGuess.Clear();
            }
        }
    }
}
