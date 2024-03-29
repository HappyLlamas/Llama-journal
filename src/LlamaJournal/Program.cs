using BusinnesLayer.Services;
using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
});

// Add controllers and html templates
builder.Services.AddRazorPages();
builder.Services.AddAuthentication();

// Add DB context
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));


// Add services and repositories

builder.Services.AddDataLayerServices();
builder.Services.AddBusinessLayerServices();
builder.Services.AddScoped<GradesService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
	options => 	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login");
		options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/");
	}
);
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

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

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
