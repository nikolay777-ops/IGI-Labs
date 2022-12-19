using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;
using WEB_0535005_Vashkevich.Models;
using WEB_0535005_Vashkevich.Services;
using WEB_0535005_Vashkevich.Middleware;
using Microsoft.Build.Execution;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Host.ConfigureLogging(log =>
{
    var logPath = $"Logs/Log-[{DateTime.Today:dd.MM.yyyy}].txt";

    log.ClearProviders();

    log.AddProvider(new FileLoggerProvider(logPath));
    log.AddFilter("Microsoft", LogLevel.None);
    log.AddFilter("System", LogLevel.None);

});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = false;
}
).AddDefaultUI()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath= $"/Identity/Account/Logout";
});

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<Cart>(sp => CartService.GetCart(sp));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});

builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

app.UseSession();
app.UseMyMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
        );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.MapRazorPages();

app.Services.CreateScope().ServiceProvider.GetRequiredService<IDbInitializer>().Initialize();

app.Run();
