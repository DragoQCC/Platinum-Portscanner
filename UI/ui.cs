using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;


namespace Platinum_portscanner.UI
{
    public class ui
    {
        public static void banner()
        {
            Console.WriteLine(
              " ██████╗ ██╗      █████╗ ████████╗██╗███╗   ██╗██╗   ██╗███╗   ███╗    ██████╗  ██████╗ ██████╗ ████████╗\n".Pastel(Color.FromArgb(186, 183, 178)) +
              " ██╔══██╗██║     ██╔══██╗╚══██╔══╝██║████╗  ██║██║   ██║████╗ ████║    ██╔══██╗██╔═══██╗██╔══██╗╚══██╔══╝\n".Pastel(Color.FromArgb(186, 183, 178)) +
              " ██████╔╝██║     ███████║   ██║   ██║██╔██╗ ██║██║   ██║██╔████╔██║    ██████╔╝██║   ██║██████╔╝   ██║   \n".Pastel(Color.FromArgb(186, 183, 178)) +
              " ██╔═══╝ ██║     ██╔══██║   ██║   ██║██║╚██╗██║██║   ██║██║╚██╔╝██║    ██╔═══╝ ██║   ██║██╔══██╗   ██║   \n".Pastel(Color.FromArgb(186, 183, 178)) +
              " ██║     ███████╗██║  ██║   ██║   ██║██║ ╚████║╚██████╔╝██║ ╚═╝ ██║    ██║     ╚██████╔╝██║  ██║   ██║   \n".Pastel(Color.FromArgb(186, 183, 178)) +
              " ╚═╝     ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝    ╚═╝      ╚═════╝ ╚═╝  ╚═╝   ╚═╝  \n".Pastel(Color.FromArgb(186, 183, 178)) +
                "\n" +
               "Author: Jon @QueenCityCyber \n".Pastel(Color.SkyBlue) +
               "https://github.com/Queen-City-Cyber \n".Pastel(Color.FromArgb(52, 152, 52))
                );
        }

    }
}
