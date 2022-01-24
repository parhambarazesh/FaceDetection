using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace playbook
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var httpClient = new HttpClient();
            var urlAddress = "https://api.covid19api.com/summary";
            var response = await httpClient.GetAsync(urlAddress);
            var apiResponse = await response.Content.ReadAsStringAsync();
            JObject res = JObject.Parse(apiResponse);
            JArray data = (JArray)res["Countries"];
            var queriedData = data.Where(c => (string)c["Country"] == "Norway");
            Console.WriteLine(queriedData.ToList()[0]);
        }
    }
}
