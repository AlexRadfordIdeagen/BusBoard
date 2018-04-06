using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.tfl.gov.uk/StopPoint/490008660N");
            client.Authenticator = new HttpBasicAuthenticator("17f019cd", "e2d6baee3b1ff00d10071cdd67a96bac");

            var request = new RestRequest("Arrivals", Method.GET);

            var responseList = client.Execute<List<BusArrivals>>(request);
            PrintArrivals(responseList);

            Console.ReadLine();
        }

        private static void PrintArrivals(IRestResponse<List<BusArrivals>> responseList)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Expected Arrival: " + responseList.Data.ElementAt(i).ExpectedArrival.ToLocalTime().TimeOfDay + " |||| " + "Vehicle Id: " + responseList.Data.ElementAt(i).VehicleId);
            }
        }
    }


}  