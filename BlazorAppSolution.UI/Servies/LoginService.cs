using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;
        private const string LoginEndpoint = "https://actbackendseervices.azurewebsites.net/api/login"; // API endpoint

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        // LoginAsync method to handle user login
        public async Task<string?> LoginAsync(Login credentials)
        {
            if (credentials == null) throw new ArgumentNullException(nameof(credentials));

            try
            {
                // Send POST request to the API with the login credentials
                var response = await _httpClient.PostAsJsonAsync(LoginEndpoint, credentials);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response and get the token
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginRespond>();
                    var token = loginResponse?.Token;

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        // Save the token securely
                        await SaveTokenAsync(token);
                        return token; // Return the token if successful
                    }

                    Console.WriteLine("Login response does not contain a valid token.");
                }
                else
                {
                    // Handle failed login attempts
                    Console.WriteLine($"Login failed: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle network issues
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            return null;
        }

        public async Task AddAuthHeaderAsync()
        {
            try
            {
                // Get the token securely
                var token = await GetTokenAsync();

                if (!string.IsNullOrWhiteSpace(token))
                {
                    // Add the token to the Authorization header
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    // Handle missing token
                    Console.WriteLine("Token not found or is empty.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions when accessing the token
                Console.WriteLine($"Error retrieving token: {ex.Message}");
            }
        }

        private async Task SaveTokenAsync(string token)
        {
            // Use a secure storage mechanism suitable for Blazor
            await Task.Run(() => Console.WriteLine($"Token saved: {token}"));
        }

        private async Task<string?> GetTokenAsync()
        {
            // Simulate retrieval from secure storage
            return await Task.FromResult("dummy_token");
        }
    }
}