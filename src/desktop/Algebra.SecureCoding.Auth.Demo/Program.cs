using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;



var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider();

var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
var authService = new AuthenticationService(httpClientFactory);
var protectedService = new ProtectedDataService(httpClientFactory);

var token=await authService.LoginAsync("ivor.maric@gmail.com", "Pass123$");
if (token!=null)
{
    Console.WriteLine($"Access Token: {token.AccessToken}");
    Console.WriteLine($"Refresh Token: {token.RefreshToken}");
    Console.WriteLine($"Exp date: {token.ExpDate.ToLocalTime()}");
}
while (true)
{
    if (DateTime.UtcNow.AddSeconds(30) >= token.ExpDate) 
    {
        Console.WriteLine("Token is about to expire. Refreshing...");
        token = await authService.RefreshToken(token.RefreshToken);
        Console.WriteLine($"New Access Token: {token.AccessToken}");
    }

    Console.WriteLine("Protected data:");
    Console.WriteLine(await protectedService.GetProtectedDataAsync(token.AccessToken)); 
    await Task.Delay(10000);
}


public class AuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthenticationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var client = _httpClientFactory.CreateClient();
        var loginRequest = new { Email = email, Password = password };
        var response = await client.PostAsync("https://localhost:44304/api/Authentication/Login", new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(json);
        }

        return null;
    }

    public async Task<TokenResponse> RefreshToken(string refreshToken)
    {
        var client = _httpClientFactory.CreateClient();
        var refreshRequest = new { RefreshToken= refreshToken };
        var response = await client.PostAsync("https://localhost:44304/api/Authentication/RefreshToken", new StringContent(JsonConvert.SerializeObject(refreshRequest), Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResponse>(json);
        }

        return null;
    }
}


public class ProtectedDataService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProtectedDataService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetProtectedDataAsync(string token)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("https://localhost:44304/api/ProtectedData");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        return "Access Denied";
    }
}


public class TokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpDate { get; set; }
}