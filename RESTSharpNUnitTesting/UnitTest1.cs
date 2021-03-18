using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RESTSharpNUnitTesting
{
    public class Employee
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
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
        /// <summary>
        /// TC:1 retrieves number of people in file
        /// </summary>
        [Test]
        public void ReturnGivenEmployeeList()
        {
            IRestResponse response = getEmpoylee();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> list = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(10, list.Count);

        }
        /// <summary>
        /// TC2: Add employee
        /// </summary>
        [Test]
        public void GivenEmployeeOnpostShouldreturnAddEmployee()
        {
           RestRequest request = new RestRequest("/Employee", Method.POST);
            JObject jObject = new JObject();
            jObject.Add("Name", "Pradip");
            jObject.Add("Salary", 20000);
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Pradip", dataresponse.Name);
            Assert.AreEqual("20000", dataresponse.Salary);
        }
        /// <summary>
        /// TC3: Edit the details of perticular employee 
        /// </summary>
        [Test]
        public void GivenEmployeeOnUpdateReturnUpdateemployee()
        {
            RestRequest request = new RestRequest("/Employee/9", Method.PUT);
            JObject jObject = new JObject();
            jObject.Add("Name", "Ganesh");
            jObject.Add("Salary", 50000);

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataresponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Ganesh", dataresponse.Name);
            Assert.AreEqual("50000", dataresponse.Salary);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// TC5:Delete the person details
        /// </summary>
        [Test]
        public void GivenEmployeeIDOnDeleteShouldReturnsucessFulstatus()
        {
            RestRequest request = new RestRequest("/Employee/7", Method.DELETE);


            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Console.WriteLine(response.Content);

        }
    }
}