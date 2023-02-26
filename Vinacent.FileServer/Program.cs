using Microsoft.EntityFrameworkCore;
using Vinacent.FileServer.Abstracts;
using Vinacent.FileServer.Data;
using Vinacent.FileServer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dbcontext
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FileServerDbContext>(x => x.UseSqlServer(connectionString));

// Add DI
builder.Services.AddTransient<IFileProcessAppService, FileProcessAppService>();

// CORS
var corsPolicyName = "CORS_Policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, builder => {
        builder.WithOrigins("https://localhost:44312").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "vinacent.com");
        //builder.SetIsOriginAllowed(origin => true);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
