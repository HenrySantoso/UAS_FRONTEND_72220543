using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Services
{
    public class EnrollmentService
    {
        private static readonly string BaseAddress = "https://actbackendseervices.azurewebsites.net"; // Base address for the API
        private static readonly string Url = $"{BaseAddress}/api/enrollments"; // Endpoint for enrollments
        private readonly HttpClient _httpClient;

        public EnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET ALL
        public async Task<List<Enrollment>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(Url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Enrollment>>(json) ?? new List<Enrollment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching enrollments: {ex.Message}");
                return new List<Enrollment>();
            }
        }

        // GET BY ID
        public async Task<Enrollment?> GetByIdAsync(int enrollmentId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Url}/{enrollmentId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Enrollment>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching enrollment by ID: {ex.Message}");
                return null;
            }
        }

        // POST (CREATE)
        public async Task<bool> CreateAsync(EnrollmentAdd newEnrollment)
        {
            try
            {
                var json = JsonSerializer.Serialize(newEnrollment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(Url, content);

                // Log the HTTP status code and content for debugging
                if (!response.IsSuccessStatusCode)
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}, Response: {errorResponse}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating enrollment: {ex.Message}");
                // You can log the entire exception for more details
                Console.WriteLine(ex.ToString());
                return false;
            }
        }



        // PUT (UPDATE)
        public async Task<bool> UpdateAsync(int enrollmentId, Enrollment updatedEnrollment)
        {
            try
            {
                var json = JsonSerializer.Serialize(updatedEnrollment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{Url}/{enrollmentId}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating enrollment: {ex.Message}");
                return false;
            }
        }

        // DELETE
        public async Task<bool> DeleteAsync(int enrollmentId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{Url}/{enrollmentId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting enrollment: {ex.Message}");
                return false;
            }
        }
    }
}
