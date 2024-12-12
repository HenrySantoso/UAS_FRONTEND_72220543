namespace BlazorAppSolution.UI.Model;

class LoginRespond
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; internal set; }
}
