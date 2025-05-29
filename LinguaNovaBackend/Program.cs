using Microsoft.OpenApi.Models;
using LinguaNova.Services;
using Microsoft.EntityFrameworkCore;
using LinguaNovaBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5040);
    options.ListenAnyIP(5041, listenOptions =>
    {
        listenOptions.UseHttps();
    } );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Language Learning API", Version = "v1" });
});
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services
builder.Services.AddScoped<GeminiService>();

// Add HttpClient
builder.Services.AddHttpClient<GeminiService>();

// Add DbContext configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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
app.UseCors("AllowAll");
app.UseStaticFiles();
//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
