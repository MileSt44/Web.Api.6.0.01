using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneBookApi.Models;
using Web.Api._6._0._01.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<ISenderService, SenderService>();

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
