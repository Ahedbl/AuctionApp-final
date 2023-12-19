using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AuctionApp.Data;
using AuctionApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionSqlPersistence>();

// Database connection
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDbConnection")));

// Identity
builder.Services.AddDbContext<AuctionAppIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionAppIdentityContextConnection")));
builder.Services.AddDefaultIdentity<AuctionAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuctionAppIdentityContext>();

// Automapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
