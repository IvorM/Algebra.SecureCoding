using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString));

builder.Services.TryAddScoped<IHardwareService,HardwareServices>();
//builder.Services.TryAddScoped<IReviewService, ReviewService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.EnsureSeedData();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
