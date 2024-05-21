using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data.UnitOfWork;
using ProjectManagement.Data;
using Microsoft.OpenApi.Models;
using ProjectManagement.Business;
using ProjectManagement.Core.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); ;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Data Source=gizem\\SQLEXPRESS;Initial Catalog=ProjectManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
 
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
