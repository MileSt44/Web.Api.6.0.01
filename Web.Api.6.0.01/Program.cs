using Microsoft.EntityFrameworkCore;
using PhoneBookApi.Models;
using Web.Api._6._0._01.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<ISenderService, SenderService>();

builder.Services.AddDbContext<PhonebookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhonebookContext")));

var app = builder.Build();



if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
