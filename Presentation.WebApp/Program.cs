using Application.Bookings;
using Application.Extensions;
using Domain.Abstractions.Repositories;
using Infrastructure.Extensions;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Context.Extensions;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddApplication(builder.Configuration, builder.Environment);

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IGymClassRepository, GymClassRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. IDENTITY KONFIGURATION
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? "google-dummy-id";
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? "google-dummy-secret";
    })
    .AddGitHub(options =>
    {
        options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"] ?? "github-dummy-id";
        options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"] ?? "github-dummy-secret";

        options.Scope.Add("user:email");
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Initiera databasen
await PersistenceDatabaseInitializer.Initialize(app.Services, app.Environment);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();