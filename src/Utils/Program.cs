using System;
using Kuda.Logistics.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Kuda.Domain.Security;

namespace Kuda.Logistics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nThis utility will prepare a Kuda API request that you can use in your clients (e.g Postman).\n");

            var clientKey = "19vaYTfecC5mRdoM4rKD";
            var rand = new Random();
            var randomString = rand.Next(0, Int32.MaxValue);
            var password = $"{clientKey}-{randomString}";

            Console.WriteLine("\nStep 1: Include the following encrypted value as a header parameter.\n");
            Hasher.EncryptRSA(password);
            Console.WriteLine($"\"password\" : {password}");

            Console.WriteLine("\nStep 2: Include the following encrypted value as a header parameter with name \"password\".\n");

            var serviceType = "FUND_VIRTUAL_ACCOUNT";

            var request = new RequestModel(serviceType);
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            Console.WriteLine(JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }));

        }
    }
}
