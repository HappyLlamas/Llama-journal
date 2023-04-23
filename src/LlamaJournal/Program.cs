using Microsoft.EntityFrameworkCore;
using DataLayer.Models;

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

app.MapRazorPages();

app.Run();
