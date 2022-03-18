# Platinum Portscanner
Platinum portscanner is a C# tool for scanning hosts and ports, it has basic port service detection based on the commonly ran service for a port number(always verify) and basic host OS guesses. It was created to mainly be used once insuide a windows host but still wanting a portscanner with easy syntax, speed and output. 
![image](https://user-images.githubusercontent.com/15575425/159026346-0ba8e068-88ad-4d16-906a-e5a6e761adfd.png)


## usage
Program comes with one main command at the moment with two required options. tcpscan /hosts (values) /ports (values) and an optional /nocheck to skip the host detection phase. Users can load it into memory with the below instructions similar to a powershell script and then perform fast, reliable and useful potscanning in memory of a compromised host. 

### the help menu
![image](https://user-images.githubusercontent.com/15575425/159027184-f9e6b874-6be7-47de-a184-57ae644752cc.png)


/hosts can be done in 3 ways 
- list like 127.0.0.1 127.0.0.2 127.0.0.3 etc.
- range like 127.0.0.1-100 or 127.0.0.100-255
- a cidr notation like 127.0.0.1\24 (atm only supports \24-\32 need to work updating for bigger sized cidr ranges)

/ports can be done in 3 ways
- a list like 80 22 53 8080 etc.
- a range like 1-1000
- also a custom top-50 option from the most common ports ive come across during engagements and labs

/nocheck has the option of yes that disables the host up check and just scans the whole of what the user enters like a whole \24 for the selected ports.

### speed and output ?
Platnium port scanner has so far been just as fast if not faster then nmap for host discovery and port scanning, while as not robust if the goal is just to find open ports and running hosts this program has the speed.
![image](https://user-images.githubusercontent.com/15575425/159028376-6ecfbb7c-642f-4afd-b652-4997853f182e.png)
this is a whole \24 check and 1k port scan. Mine on the left vs a -t5 nmap scan on the right. 

output is also tries to be clean and helpful
![image](https://user-images.githubusercontent.com/15575425/159028542-b1c54f16-b724-4b1a-b3ce-32dd73157185.png)


### In memory loading ?
1. "$data = (New-Object System.Net.WebClient).DownloadData('http://127.0.0.1:8081/Platinum_Portscanner.exe')"
2. "$assem =[System.Reflection.Assembly]::Load($data)"
3. "[Platinum_Portscanner.Program]::Main()"
** NOTE: the double quotes are just for formatting do not include when running command**

## features
- fast scanning time 
- verbose output
- in memory loading 

## disclaimer
I am not responsible for actions taken by users of Platinum portscanner. Platinum portscanner was released solely for educational and ethical purposes.
