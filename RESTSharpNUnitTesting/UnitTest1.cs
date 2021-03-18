using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RESTSharpNUnitTesting
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }
    public class Tests
    {
        RestClient client;
        [SetUp]
         public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
        
        public IRestResponse getEmpoylee()
        {
            RestRequest request = new RestRequest("/Employee", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }
       
        [Test]
        public void Return_GivenEmployeeList()
        {
            IRestResponse response = getEmpoylee();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(8, list.Count);

        }
    }
}