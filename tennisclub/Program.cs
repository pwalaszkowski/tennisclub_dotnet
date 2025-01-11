using Microsoft.EntityFrameworkCore;
using tennisclub.Controllers.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession();
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.LoginPath = "/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Session expires after 30 minutes
        config.SlidingExpiration = true; // Renew session on activity
    });

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // Secure all pages
    options.Conventions.AllowAnonymousToPage("/Login"); // Allow access to Login page
    options.Conventions.AllowAnonymousToPage("/Register"); // Allow access to Register page
});

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

// Redirect the root URL to the Login page
app.MapGet("/", context => Task.Run(() => context.Response.Redirect("/Login")));

app.MapRazorPages();

app.Run();
