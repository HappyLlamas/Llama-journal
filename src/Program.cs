using Microsoft.EntityFrameworkCore;
using llama_journal.Models;
using Database;
using llama_journal.Data.Repositories;
using llama_journal.Services;


var builder = WebApplication.CreateBuilder(args);

// Add controllers and html templates
builder.Services.AddRazorPages();
builder.Services.AddAuthentication();


// Add DB context
builder.Services.AddDbContext<ModelsContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddControllersWithViews();

// Add services and repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

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
		pattern: "{controller=Progress}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
