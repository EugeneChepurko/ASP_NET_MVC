using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_MVC.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace ASP_NET_MVC.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string regionCode = this.HttpContext.Items["regions"].ToString();
            ViewData["Message"] = "Your region - " + regionCode;
            ViewData["MessageLocalIP"] = "Your local IP - " + GetLocalIPAddress();
            ViewData["MessageMACAddress"] = "Your MAC address - " + GetMacAddress();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            {
                foreach (var ip in host.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    { return ip.ToString(); }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        private static string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up).Select(nic => nic.GetPhysicalAddress().ToString()).FirstOrDefault();
        }
    }
}