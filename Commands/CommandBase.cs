using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum_portscanner.Commands
{
    public abstract class CommandBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public static List<int> Ports { get; set; }
        public static List<int> OpenPorts { get; set; } = new List<int>();

        public static Dictionary<string, List<int>> ConnctionDictionary { get; set; } = new Dictionary<string, List<int>>();
        public static Dictionary<string, string> HostOSGuess { get; set; } = new Dictionary<string, string>();
        public static List<string> Hosts { get; set; }
        public static List<string> AwakeHosts { get; set; } = new List<string>();
        public static bool NoCheck { get; set; } = false;

        public abstract void HostUp();
        public abstract void Scan();

        //a dictionary of top 50 most commonly used unique computer ports and the corrosponding service
        // current count : 50
        public static Dictionary<int, string> PortDictionary = new Dictionary<int, string>()
        {
            {20, "FTP" },
            {21, "FTP"},
            {22, "SSH"},
            {23, "Telnet"},
            {25, "SMTP"},
            {53, "DNS"},
            {80, "HTTP"},
            {88, "kerberos"},
            {110, "POP3"},
            {111, "rpcbind" },
            {115,"SFTP" },
            {135, "msrpc" },
            {137, "netbios-ns" },
            {139, "netbios-ssn" },
            {143, "IMAP"},
            {389, "LDAP" },
            {443, "HTTPS"},
            {445, "SMB"},
            {464, "kpasswd5" },
            {515, "printer"},
            {543, "kerberos login"},
            {636, "ldap ssl" },
            {749, "kerberos-adm" },
            {902 , "vmware server" },
            {993, "imap ssl" },
            {995, "pop3 ssl"},
            {1433, "ms-sql" },
            {2082,"cpanel" },
            {2083,"cpanel ssl" },
            {3268, "AD global-cat ldap" },
            {3269, "AD global-cat ldap ssl" },
            {3306, "MySQL"},
            {3389, "RDP"},
            {5900, "VNC"},
            {5985,"winrm http" },
            {5986,"winrm https" },
            {6602, "windows wsscom" },
            {8080, "HTTP"},
            {8443, "HTTPS"},
            {8888, "HTTP"},
            {8912, "windows client backup" },
            {9090, "HTTP"},
            {9200, "Elasticsearch"},
            {9300, "Elasticsearch"},
            {9389, "AD web services" },
            {10000, "http" },
            {10001, "http" },
            {10002, "http" },
            {10003, "http" },
            {10004, "http" },
            {10005, "http" }
        };

    }
}
