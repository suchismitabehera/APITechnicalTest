using System.IO;
using System.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnitTestProjectAPITest.Response;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Configuration;

namespace NUnitTestProjectAPITest
{
    public class Tests
    {
        RestClient client;
        string apiKey;
        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://petstore.swagger.io/v2");
            apiKey = ConfigurationManager.AppSettings["ApiKey"];
        }

        
        /// <summary>
        /// Test to validate POST operation
        /// </summary>
        [Test]
        public void Test1()
        {
            Pet pet = JsonConvert.DeserializeObject<Pet>(File.ReadAllText(@"..\..\..\Requests\POST.json"));
           // Pet pet = JsonConvert.DeserializeObject<Pet>(File.ReadAllText(@"C:\Shubhranshu\Suchismita\Ding\NUnitTestProjectAPITest\Requests\POST.json"));
            RestRequest request = new RestRequest("/pet", Method.POST);
            request.AddHeader("api_key", apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(pet);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Pet petResponse = new JsonDeserializer().Deserialize<Pet>(response);
            Assert.That(petResponse.id,Is.EqualTo(1));
        }

        /// <summary>
        /// Test to validate Get operation
        /// </summary>
        [Test]
        public void Test2()
        {
            RestRequest request = new RestRequest("/pet/1", Method.GET);
            request.AddHeader("api_key", apiKey);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Test to validate PUT operation
        /// </summary>
        [Test]
        public void Test3()
        {
            Pet pet = JsonConvert.DeserializeObject<Pet>(File.ReadAllText(@"..\..\..\Requests\PUT.json"));
            RestRequest request = new RestRequest("/pet", Method.PUT);
            request.AddHeader("api_key", apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(pet);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        /// <summary>
        /// Test to validate DELETE operation
        /// </summary>
        [Test]
        public void Test4()
        {
            RestRequest request = new RestRequest("/pet/1", Method.DELETE);            
            request.AddHeader("api_key", apiKey);
            IRestResponse response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}