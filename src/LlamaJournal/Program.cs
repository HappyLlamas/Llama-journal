using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add controllers and html templates
builder.Services.AddRazorPages();
builder.Services.AddAuthentication();

// Add DB context
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddControllersWithViews();

// Add services and repositories

builder.Services.AddDataLayerServices();
builder.Services.AddBusinessLayerServices();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

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

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
        pattern: "{controller=Login}/{action=Index}/{id?}");
});

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
