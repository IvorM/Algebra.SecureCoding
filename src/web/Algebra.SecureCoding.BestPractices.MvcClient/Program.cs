using Algebra.SecureCoding.BestPractices.MvcClient.Services;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookies";
    options.DefaultChallengeScheme = "oidc";
})
                .AddCookie("cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.ClientId = "mvc";
                    options.MapInboundClaims = true;
                    options.SaveTokens = true;
                    options.ResponseType = "code";
                    options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
                    options.Scope.Add("profile");
                    options.Scope.Add("openid");
                    options.Scope.Add("api.read");
                    options.Scope.Add("offline_access");
                    options.UseTokenLifetime=true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });


builder.Services.AddAccessTokenManagement();
builder.Services.AddHttpClient<IApiService, ApiService>(options =>
{
    options.BaseAddress = new Uri("https://localhost:44312/");
}).AddUserAccessTokenHandler();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
