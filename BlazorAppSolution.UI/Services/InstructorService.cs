using BlazorAppSolution.UI.Model;

namespace BlazorAppSolution.UI.Services;

public class InstructorService
{

    private static readonly string BaseAddress = "https://actbackendseervices.azurewebsites.net"; // Base address for the API
    private static readonly string InstructorUrl = $"{BaseAddress}/api/instructors"; // Endpoint for enrollments
    private readonly HttpClient _httpClient;

    public InstructorService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Enrollment-related methods
    public async Task<List<Instructor>> GetAllInstructorsAsync() =>
        await _httpClient.GetFromJsonAsync<List<Instructor>>(InstructorUrl);
}
