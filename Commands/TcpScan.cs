using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Pastel;
using System.Drawing;
using System.Net;

namespace Platinum_portscanner.Commands
{
    public class TcpScan : CommandBase
    {
        public override string Name => "TcpScan";
        public override string Description => "Allows for port scanning of tcp ports.";

        //static init function to start host check and scan
        public void init()
        {
            //start host check
            DateTime HostCheckStart = DateTime.Now;
            if (NoCheck == false)
            {
                HostUp();
            }
            else
            {
                Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)}Host check skipped.");
            }
            DateTime ScanStart = DateTime.Now;
            //start scan
            Scan();
            DateTime ScanEnd = DateTime.Now;
            Console.WriteLine($"\n{"[*]".Pastel(Color.SkyBlue)}Scanning completed in " + (ScanEnd - ScanStart).TotalSeconds + " seconds.");
            Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)}Host check and scan completed in " + (ScanEnd - HostCheckStart).TotalSeconds + " seconds.\n");
            //print results
            Print();
        }

        public override void HostUp()
        {

            // parallel foreach loop to send a ping to every hosts in the CommandBase Hosts list if the PingReply status is successfully pring that the host is up add them to the CommandBase AwakeHosts List and return true
            Parallel.ForEach(Hosts, host =>
            {
                var ping = new Ping();
                var reply = ping.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    AwakeHosts.Add(host);
                    Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)} {host} is awake");
                    // if ping ttl is 127 or 128 add host to the CommandBase HostOSGuess list as windows if it is 63 or 64 add it as linux otherise dont add it to the list
                    if (reply.Options.Ttl == 127 || reply.Options.Ttl == 128)
                    {
                        HostOSGuess.Add(host, "windows");
                        //Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)} {host} is a windows host");
                    }
                    else if (reply.Options.Ttl == 63 || reply.Options.Ttl == 64)
                    {
                        HostOSGuess.Add(host, "linux");
                        //Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)} {host} is a linux host");
                    }
                }
            });
            Console.WriteLine($"{"[+]".Pastel(Color.Green)} {AwakeHosts.Count} hosts are up.");

        }

        public override void Scan()
        {
            //if nocheck is true set AwakeHosts to Hosts
            if (NoCheck == true)
            {
                AwakeHosts = Hosts;
            }

            Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)}Scanning...\n");
            bool IsVerbose = false;
            //using async connect scan each awake host for Ports in Ports list and add them to the CommandBase OpenPorts List
            Parallel.ForEach(AwakeHosts, host =>
            {
                List<int> tempOpenPorts = new List<int>();
                foreach (int port in Ports.ToList())
                {
                    using (var tcpClient = new TcpClient())
                    {

                        try
                        {
                            var tsk = tcpClient.ConnectAsync(host, port);
                            tsk.Wait(10);
                            if (tsk.IsCompleted)
                            {
                                tempOpenPorts.Add(port);
                                //if v key is pressed print the port
                                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.V)
                                {
                                    Console.WriteLine($"{"[+]".Pastel(Color.Green)} verbose mode activated");
                                    IsVerbose = true;
                                }
                                if (IsVerbose)
                                {
                                    Console.WriteLine($"{"[+]".Pastel(Color.Green)} {host}:{port} is open");
                                }

                            }
                        }
                        catch (Exception)
                        {
                            tcpClient.Close();
                            continue;
                        }
                    }
                }
                ConnctionDictionary[host] = tempOpenPorts;
            });
            Console.WriteLine($"{"[+]".Pastel(Color.Green)} scanning is done");
        }

        //function that will print all the open ports and tell the user the port is open
        public static void Print()
        {
            Console.WriteLine($"{"[*]".Pastel(Color.SkyBlue)}Printing results...\n");
            //print the contents of the ConnectionDictionary including its lists
            foreach (var host in ConnctionDictionary)
            {
                if (HostOSGuess.ContainsKey(host.Key))
                {
                    Console.WriteLine($"{"[+]".Pastel(Color.Green)} {host.Key} is likely a {HostOSGuess[host.Key]} host");
                }
                else
                {
                    Console.WriteLine($"{"[+]".Pastel(Color.Green)} Host {host.Key}:");
                }

                //print number of closed ports for the host
                Console.WriteLine($"{"[-]".Pastel(Color.Red)} {Ports.Count - host.Value.Count} closed ports");
                // print number of open ports for the host
                Console.WriteLine($"{"[+]".Pastel(Color.Green)} {host.Value.Count} open ports");

                //parallel foreach loop to print the open ports for the host and the corresponding service from PortDictionary if port is not found in dictionary just print the port 
                Parallel.ForEach(host.Value, port =>
                {
                    if (PortDictionary.ContainsKey(port))
                    {
                        Console.WriteLine($"{"[+]".Pastel(Color.Green)} port {port} is open likely ({PortDictionary[port]})");
                    }
                    else
                    {
                        Console.WriteLine($"{"[+]".Pastel(Color.Green)} {port} is open");
                    }
                });


                Console.WriteLine(" ");
            }
        }

    }
}
