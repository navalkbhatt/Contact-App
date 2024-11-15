using ContactApp.Application.UseCases;
using ContactAppApi.Extensions.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://*:5000");
//Add methods Extensions
builder.Services.AddInjectionApplication();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Specify the allowed origin
                   .AllowAnyHeader() // Allow any headers (adjust as needed)
                   .AllowAnyMethod() // Allow any HTTP methods (adjust as needed)
                   .AllowCredentials(); // Allow credentials (if applicable)
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost4200");
app.AddMiddleware();

app.MapControllers();

app.Run();
