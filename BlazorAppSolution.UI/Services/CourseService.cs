using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppSolution.UI.Model; // Adjust the namespace according to your project structure

namespace BlazorAppSolution.UI.Services
{
    public class CourseService
    {
        private readonly HttpClient _httpClient;
        private const string CourseApiUrl = "https://actbackendseervices.azurewebsites.net/api/courses";
        private const string CategoryApiUrl = "https://actbackendseervices.azurewebsites.net/api/categories"; // New API URL for categories

        public CourseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all courses
        public async Task<List<Course>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Course>>(CourseApiUrl);
        }

        // Get a course by ID
        public async Task<Course> GetCourseByIDAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Course>($"{CourseApiUrl}/{id}");
        }

        // Create a new course
        public async Task<Course> CreateCourseAsync(Course course)
        {
            var response = await _httpClient.PostAsJsonAsync(CourseApiUrl, course);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Course>();
        }

        // Update an existing course
        public async Task<Course> UpdateCourseAsync(int id, Course course)
        {
            var courseToUpdate = new
            {
                course.Name,
                course.Description,
                course.Duration,
                course.ImageName,
                CategoryId = course.Category?.CategoryId
            };

            var response = await _httpClient.PutAsJsonAsync($"{CourseApiUrl}/{id}", courseToUpdate);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Course>();
        }


        // Delete a course
        public async Task DeleteCourseAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{CourseApiUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        // New method to get all categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Category>>(CategoryApiUrl); // Fetch categories from the new API URL
        }

        // New method to get a course by Name
        public async Task<List<Course>> GetCourseByNameAsync(string courseName)
        {
            var response = await _httpClient.GetAsync($"{CourseApiUrl}/search/{courseName}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Course>>() ?? new List<Course>();
            }
            throw new Exception($"Failed to fetch courses: {response.ReasonPhrase}");   
        }

    }
}
