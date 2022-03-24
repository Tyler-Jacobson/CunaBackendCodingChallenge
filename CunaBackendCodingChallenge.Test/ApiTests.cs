using CunaBackendCodingChallenge.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CunaBackendCodingChallenge.Test
{

    [TestClass]
    public class ApiTests
    {
        // I feel this is a good place to take a shortcut, as mentioned in the instructions. Here I'm running the tests against the actual database.
        // Ideally I'd be creating a mock database using SQLite or something similar, and running the tests against that

        [TestMethod]
        public async Task ClientRequest_Post_ReturnsStatusCode200()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var clientRequest = new ClientRequestDto("Test Body");
            var json = JsonConvert.SerializeObject(clientRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/ClientRequest", data);

            var txtResponse = await response.Content.ReadAsStringAsync(); // this will return the url
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(txtResponse);
            //ClientRequestId = responseDictionary["id"];
            string ClientRequestId = responseDictionary["id"];

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod]
        public async Task Get_ClientByExistingId_ReturnsStatusCode200()
        {

            // Arrange
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            // Act
            var response = await httpClient.GetAsync("/api/ClientRequest/1");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_ClientByNonExistantId_ReturnsStatusCode404()
        {
            // Arrange
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            // Act
            var response = await httpClient.GetAsync("/api/ClientRequest/102");

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public async Task Get_OnSuccess_ReturnsTypeOfClientRequest()
        {
            // Arrange
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            // Act
            var response = await httpClient.GetAsync("/api/ClientRequest/1");
            var stringResult = response.Content.ReadAsStringAsync().Result;

            ClientRequest clientRequest = JsonConvert.DeserializeObject<ClientRequest>(stringResult);


            // Assert
            Assert.IsInstanceOfType(clientRequest, typeof(ClientRequest));
        }

        class wrongFormatClientRequest
        {
            public wrongFormatClientRequest(string incorrectFormatBody)
            {
                this.notBody = incorrectFormatBody;
            }

            public string notBody;
        }

        [TestMethod]
        public async Task Post_InvalidClientRequestBody_ReturnsStatusCode400()
        {

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var clientRequest = new wrongFormatClientRequest("wrong format");
            var json = JsonConvert.SerializeObject(clientRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // the endpoint expects 'body', this request instead sends 'notBody'
            var response = await httpClient.PostAsync("/api/ClientRequest", data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        }

        class wrongTypeClientRequest
        {
            public wrongTypeClientRequest(int incorrectFormatBody)
            {
                this.body = incorrectFormatBody;
            }

            public int body;
        }

        [TestMethod]
        public async Task Post_InvalidClientRequestType_ReturnsStatusCode400()
        {

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var clientRequest = new wrongTypeClientRequest(1);
            var json = JsonConvert.SerializeObject(clientRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // the endpoint expects 'body' to be a string, this request instead sends an int
            var response = await httpClient.PostAsync("/api/ClientRequest", data);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task Post_ServiceReport_ReturnsStatusCode200()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var clientRequest = new ClientRequestDto("Test Body");
            var clientJson = JsonConvert.SerializeObject(clientRequest);
            var clientData = new StringContent(clientJson, Encoding.UTF8, "application/json");

            var clientResponse = await httpClient.PostAsync("/api/ClientRequest", clientData);
            var clientTxtResponse = await clientResponse.Content.ReadAsStringAsync();
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(clientTxtResponse);

            string ClientRequestId = responseDictionary["id"];

            var serviceRequest = new CreateServiceReportDto("STARTED");
            var serviceJson = JsonConvert.SerializeObject(serviceRequest);
            var serviceData = new StringContent(serviceJson, Encoding.UTF8, "application/json");

            var serviceResponse = await httpClient.PostAsync($"/api/ServiceReport/callback/{ClientRequestId}", serviceData);

            Assert.AreEqual(HttpStatusCode.NoContent, serviceResponse.StatusCode);
        }

        [TestMethod]
        public async Task Put_ServiceReport_ReturnsStatusCode200()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var clientRequest = new ClientRequestDto("Test Body");
            var clientJson = JsonConvert.SerializeObject(clientRequest);
            var clientData = new StringContent(clientJson, Encoding.UTF8, "application/json");

            var clientResponse = await httpClient.PostAsync("/api/ClientRequest", clientData);
            var clientTxtResponse = await clientResponse.Content.ReadAsStringAsync();
            var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(clientTxtResponse);

            string ClientRequestId = responseDictionary["id"];

            var serviceRequest = new CreateServiceReportDto("STARTED");
            var serviceJson = JsonConvert.SerializeObject(serviceRequest);
            var serviceData = new StringContent(serviceJson, Encoding.UTF8, "application/json");

            var serviceRespose = await httpClient.PostAsync($"/api/ServiceReport/callback/{ClientRequestId}", serviceData);

            var serviceUpdateRequest = new UpdateServiceReportDto("COMPLETED", "Details of completion");
            var serviceUpdateJson = JsonConvert.SerializeObject(serviceUpdateRequest);
            var serviceUpdateData = new StringContent(serviceUpdateJson, Encoding.UTF8, "application/json");

            var serviceUpdateResponse = await httpClient.PutAsync($"/api/ServiceReport/callback/{ClientRequestId}", serviceUpdateData);

            Assert.AreEqual(HttpStatusCode.NoContent, serviceUpdateResponse.StatusCode);
        }

    }
}