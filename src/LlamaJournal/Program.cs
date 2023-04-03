using Microsoft.EntityFrameworkCore;
using DataLayer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and html templates
builder.Services.AddRazorPages();

// Add DB context
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
