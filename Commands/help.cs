using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;

namespace Platinum_portscanner.Commands
{
    public class help
    {
        public static void Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{"[*]".Pastel(Color.SkyBlue)}Commands are help, exit, tcpscan");
            sb.AppendLine($"{"[*]".Pastel(Color.SkyBlue)}tcpscan has 3 options /hosts /ports /nocheck\n");
           
            sb.AppendLine($"{"[*]".Pastel(Color.SkyBlue)}/hosts is a list of hosts to scan and can be input like this:\n" +
                $" /hosts 127.0.0.1 127.0.0.2 or /hosts 127.0.0.1\\24 (cidr notation but only works on /24-/32 atm) or as a list like /hosts 192.168.1.1-100");
           
            sb.AppendLine($"{"[*]".Pastel(Color.SkyBlue)}/ports is a list of ports to scan and can be input like this:\n" +
                $" /ports 80 443 or /ports 1-65535 or there is a list of top-50 ports mainly windows focused like /ports top-50\n");
            sb.AppendLine($"{"[*]".Pastel(Color.SkyBlue)}/nocheck is a boolean value to disable the host up check and can be input like this: /nocheck yes");
            Console.WriteLine(sb.ToString());
        }
    }
}
