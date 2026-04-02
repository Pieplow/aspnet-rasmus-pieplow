using Application.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddApplication(builder.Configuration, builder.Environment);

var app = builder.Build();

await PersistanceDatabaseInitializer.Initialize(app.Services, app.Environment);
app.UseStaticFiles();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
