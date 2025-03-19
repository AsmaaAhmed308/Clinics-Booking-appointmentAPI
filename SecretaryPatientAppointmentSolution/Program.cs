using Microsoft.EntityFrameworkCore;
using SecretaryPatientAppointmentSolution.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//------------------ASMAA----- strting connection with Database --------------------------
builder.Services.AddDbContext<ClinicAppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("your connection is not found "));
});
//----------------------------------------------------------------------------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// CORS Policy  
app.UseCors(policy =>
    policy.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowAnyOrigin()); // Allow all origins, headers, and methods  

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
