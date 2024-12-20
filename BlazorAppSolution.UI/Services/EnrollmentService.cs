using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Services
{
    public class EnrollmentService
    {
        private static readonly string BaseAddress = "https://actbackendseervices.azurewebsites.net"; // Base address for the API
        private static readonly string EnrollmentUrl = $"{BaseAddress}/api/enrollments"; // Endpoint for enrollments
        private readonly HttpClient _httpClient;

        public EnrollmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Enrollment-related methods
        public async Task<List<Enrollment>> GetEnrollmentsAsync() =>
            await _httpClient.GetFromJsonAsync<List<Enrollment>>(EnrollmentUrl);

        public async Task<Enrollment?> GetEnrollmentByIdAsync(int enrollmentId) =>
            await _httpClient.GetFromJsonAsync<Enrollment>($"{EnrollmentUrl}/{enrollmentId}");

        public async Task AddEnrollmentAsync(EnrollmentAdd enrollment) =>
            await _httpClient.PostAsJsonAsync(EnrollmentUrl, enrollment);

        //public async Task UpdateEnrollmentAsync(Enrollment enrollment) =>
        //    await _httpClient.PutAsJsonAsync($"{EnrollmentUrl}/{enrollment.EnrollmentId}", enrollment);

        public async Task DeleteEnrollmentAsync(int enrollmentId) =>
            await _httpClient.DeleteAsync($"{EnrollmentUrl}/{enrollmentId}");
    }
}
