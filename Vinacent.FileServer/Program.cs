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


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
