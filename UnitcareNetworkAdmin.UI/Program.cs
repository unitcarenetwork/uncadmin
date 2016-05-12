using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Microsoft.Owin.Hosting;

namespace UnitcareNetworkAdmin.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:8989";

            StartOptions startOptions  = new StartOptions(url);
            
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        private static Uri[] GetUriParams(int port)
        {
            var uriParams = new List<Uri>();

            var hostName = Dns.GetHostName();

            // Host name URI
            string hostNameUri = $"http://{Dns.GetHostName()}:{port}";

            uriParams.Add(new Uri(hostNameUri));

            // Host address URI(s)
            var hostEntry = Dns.GetHostEntry(hostName);

            foreach (var ipAddress in hostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    var addrBytes = ipAddress.GetAddressBytes();

                    string hostAddressUri = $"http://{addrBytes[0]}.{addrBytes[1]}.{addrBytes[2]}.{addrBytes[3]}:{port}";

                    uriParams.Add(new Uri(hostAddressUri));
                }
            }

            // Localhost URI
            uriParams.Add(new Uri($"http://localhost:{port}"));

            return uriParams.ToArray();
        }
    }
}
