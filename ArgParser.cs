using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platinum_portscanner.Commands;
using Pastel;
using System.Drawing;

namespace Platinum_portscanner
{
    public class ArgParser
    {
        public static List<CommandBase> Commands { get; set; } = new List<CommandBase>();

        public static bool checkInput(string input)
        {
            if (Commands.Count == 0)
            {
                loadCommandList();
            }
            List<string> inputList = input.Split(' ').ToList();
            CommandBase userCommand = Commands.FirstOrDefault(c => c.Name.Equals(input.Split(' ')[0], StringComparison.OrdinalIgnoreCase));

            if (userCommand == null)
            {
                Console.WriteLine($"{"[-]".Pastel(Color.Red)}{userCommand} is not an available commmand.");
                return false;
            }

            inputList.RemoveAt(0); // takes the command name out of the list so we can get the arguments and options. 
            Dictionary<string, List<string>> args = new Dictionary<string, List<string>>();

            //checks if arg starts with a / and if so, it adds it to the dictionary.
            for (int i = 0; i < inputList.Count(); i++)
            {
                var command = inputList[i];
                if (command.StartsWith("/"))
                {
                    var results = inputList.Skip(i + 1).TakeWhile(s => !s.Contains("/"));
                    List<string> resultList = results.ToList();
                    args.Add(command, resultList);
                }
            }
            //get values for each key
            if (!GetValues(args))
            {
                Console.WriteLine($"{"[-]".Pastel(Color.Red)} all options need values set.");
                return false;
            }
            return true;
        }

        public static bool GetValues(Dictionary<string, List<string>> arguments)
        {
            if (arguments.TryGetValue("/hosts", out List<string> hostList))
            {
                //if hostList is a cidr notation like /24 or /16, it will be converted to a list of ip addresses.
                if (hostList.Count == 1 && hostList[0].Contains("\\"))
                {
                    string cidr = hostList[0];
                    string[] cidrSplit = cidr.Split('\\');
                    int cidrSize = int.Parse(cidrSplit[1]);
                    //if cidrSize is not between 0 and 32, it will return false.
                    if (cidrSize < 0 || cidrSize > 32)
                    {
                        return false;
                    } 
                    string ip = cidrSplit[0];
                    string[] ipSplit = ip.Split('.');
                    int ipSize = ipSplit.Length;
                    int ipCount = (int)Math.Pow(2, (32 - cidrSize));
                    List<string> ipList = new List<string>();
                    for (int i = 0; i < ipCount; i++)
                    {
                        string ipAddress = "";
                        for (int j = 0; j < ipSize; j++)
                        {
                            ipAddress += ipSplit[j];
                            if (j != ipSize - 1)
                            {
                                ipAddress += ".";
                            }
                        }
                        ipSplit = ipAddress.Split('.');
                        ipSplit[ipSplit.Length - 1] = "";
                        ipAddress = string.Join(".", ipSplit);
                        ipAddress += i;

                        ipList.Add(ipAddress);
                    }
                    hostList = ipList;
                }
                //if hostList is a cidr notation of /16 through /9 it will replace the 3rd octive in the ip with the correct host numbers


                // if hostList if a range of ip address, calculate the missing ip addresses and add them to the list.
                if (hostList.Count == 1 && hostList[0].Contains("-"))
                {
                    string range = hostList[0];
                    string[] rangeSplit = range.Split('-');
                    string start = rangeSplit[0];
                    string end = rangeSplit[1];
                    string[] startSplit = start.Split('.');
                    string[] endSplit = end.Split('.');
                    int startSize = startSplit.Length;
                    int endSize = endSplit.Length;
                    int startCount = int.Parse(startSplit[startSize - 1]);
                    int endCount = int.Parse(endSplit[endSize - 1]);
                    int count = endCount - startCount;
                    List<string> ipList = new List<string>();
                    for (int i = 0; i <= count; i++)
                    {
                        string ipAddress = "";
                        for (int j = 0; j < startSize; j++)
                        {
                            ipAddress += startSplit[j];
                            if (j != startSize - 1)
                            {
                                ipAddress += ".";
                            }
                        }
                        //split ipAddress at the last . and remove the last number
                        string[] ipSplit = ipAddress.Split('.');
                        ipSplit[ipSplit.Length - 1] = "";
                        ipAddress = string.Join(".", ipSplit);
                        ipAddress += (startCount + i);

                        ipList.Add(ipAddress);
                    }
                    hostList = ipList;
                }


                CommandBase.Hosts = hostList;
            }
            else
            {
                Console.WriteLine($"{"[-]".Pastel(Color.Red)} needs host valuies assigned use /hosts value.  ex. /hosts 127.0.0.1 127.0.0.2");
                return false;
            }
            if (arguments.TryGetValue("/ports", out List<string> portList))
            {
                //if ports is a range of ports, it will be converted to a list of ports.
                if (portList.Count == 1 && portList[0].Contains("-") && !portList[0].Contains("top-"))
                {
                    string[] portSplit = portList[0].Split('-');
                    int portStart = int.Parse(portSplit[0]);
                    int portEnd = int.Parse(portSplit[1]);
                    List<string> portList2 = new List<string>();
                    for (int i = portStart; i <= portEnd; i++)
                    {
                        portList2.Add(i.ToString());
                    }
                    portList = portList2;
                }
                // if ports is a -top-50 set the Ports list to the key value of the PortDuictionary in commandBase as a string
                if (portList.Count == 1 && portList[0].Contains("top-"))
                {
                    string[] portSplit = portList[0].Split('-');
                    int portCount = int.Parse(portSplit[1]);
                    CommandBase.Ports = CommandBase.PortDictionary.Keys.Take(portCount).ToList();
                }
                else
                {
                    CommandBase.Ports = portList.Select(int.Parse).ToList();
                }



            }
            else
            {
                Console.WriteLine($"{"[-]".Pastel(Color.Red)} needs port values assigned use /ports value.  ex. /ports 80 22 53 8080");
                return false;
            }
            if (arguments.TryGetValue("/nocheck", out List<string> noCheck))
            {
                if (noCheck.ElementAt(0) == "yes")
                {
                    CommandBase.NoCheck = true;
                }
                else if (noCheck.ElementAt(0) == "no")
                {
                    CommandBase.NoCheck = false;
                }
                else
                {
                    CommandBase.NoCheck = false;
                }
            }
            return true;
        }


        public static void loadCommandList()
        {
            IEnumerable<CommandBase> exporters = typeof(CommandBase)
            .Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(CommandBase)) && !t.IsAbstract)
            .Select(t => (CommandBase)Activator.CreateInstance(t));

            Commands.AddRange(exporters);
        }
    }
}
