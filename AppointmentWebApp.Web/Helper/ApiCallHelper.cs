using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Text.Json;

namespace AppointmentWebApp.Web.Helper
{
    public class ApiCallHelper<TRequestBody>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiCallHelper(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> CallPostApiWithJwt(string jwtToken, TRequestBody requestBody, string controller, string action)
        {
            // Serialize the request body to JSON
            var jsonBody = JsonSerializer.Serialize(requestBody);
            var endpoint = _configuration["APIEndpoint"].ToString().Replace("{controller}", controller).Replace("{action}", action);
            // Create a new HttpRequestMessage
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endpoint),
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            // Add JWT token to the request header
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            // Send the request and get the response
            var response = await _httpClient.SendAsync(request);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read and return the response content
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CallGetApiWithJwt(string jwtToken, string controller, string action, string? qs = null)
        {
            var endpoint = _configuration["APIEndpoint"].ToString().Replace("{controller}", controller).Replace("{action}", action) + (qs == null ? "" : @"/" + qs);
            // Create a new HttpRequestMessage
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint)
            };

            // Add JWT token to the request header
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            // Send the request and get the response
            var response = await _httpClient.SendAsync(request);

            // Ensure the response is successful
            response.EnsureSuccessStatusCode();

            // Read and return the response content
            return await response.Content.ReadAsStringAsync();
        }
    }
}
