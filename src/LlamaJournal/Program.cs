using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
	options => //CookieAuthenticationOptions
	{
		options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/");
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
        pattern: "{controller=Login}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
