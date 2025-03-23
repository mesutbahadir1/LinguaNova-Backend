using Microsoft.OpenApi.Models;
using LinguaNova.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Language Learning API", Version = "v1" });
});

// Add services
builder.Services.AddScoped<GeminiService>();

// Add HttpClient
builder.Services.AddHttpClient<GeminiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Comment out or remove these lines if you don't want to use authentication at all
// app.UseAuthentication();
// app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
