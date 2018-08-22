using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoMain().GetAwaiter().GetResult();
        }

        static async Task DoMain()
        {
            var dis = await DiscoveryClient.GetAsync("http://localhost:5050");

            var tokenClient = new TokenClient(dis.TokenEndpoint, "ConsoleApp_id", "ConsoleApp_key");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1_scope");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var response = await client.GetAsync("http://localhost:5051/Id4Test/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
    }
}
