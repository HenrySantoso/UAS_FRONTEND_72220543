using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://actbackendseervices.azurewebsites.net";
        private const string ApiUrl = $"{BaseUrl}/api/categories"; // Corrected the ApiUrl initialization

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>(ApiUrl);
        }

        // Get a category by ID
        public async Task<Category> GetCategoryByIDAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Category>($"{ApiUrl}/{id}");
        }

        // Create a new category
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        // Update an existing category
        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{id}", category);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }

        // Delete a category
        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
