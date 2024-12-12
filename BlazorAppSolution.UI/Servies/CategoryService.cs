using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppSolution.UI.Model;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace BlazorAppSolution.UI.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage; // Inject local storage
        private const string BaseUrl = "https://actbackendseervices.azurewebsites.net";
        private const string ApiUrl = $"{BaseUrl}/api/categories";

        public CategoryService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        // Initialize Authorization Header
        private async Task InitializeAuthHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("auth_token"); // Get the token from local storage
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Get all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            await InitializeAuthHeaderAsync(); // Ensure the token is set
            return await _httpClient.GetFromJsonAsync<List<Category>>(ApiUrl);
        }

        // Get a category by ID
        public async Task<Category> GetCategoryByIDAsync(int id)
        {
            await InitializeAuthHeaderAsync(); // Ensure the token is set
            return await _httpClient.GetFromJsonAsync<Category>($"{ApiUrl}/{id}");
        }

        // Create a new category
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await InitializeAuthHeaderAsync(); // Ensure the token is set
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        // Update an existing category
        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            await InitializeAuthHeaderAsync(); // Ensure the token is set
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        // Delete a category
        public async Task DeleteCategoryAsync(int id)
        {
            await InitializeAuthHeaderAsync(); // Ensure the token is set
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
