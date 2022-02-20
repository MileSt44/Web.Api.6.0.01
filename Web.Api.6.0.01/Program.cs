using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
var app = builder.Build();
builder.Services.AddDbContext<PhonebookContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("PhonebookContext")));



if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
