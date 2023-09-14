using AdMoney.Data;
using AdMoney.Repository.Interfaces;
using AdMoney.Repository.Implementation;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<AdMoney.Data.AdMoneyContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DatabaseServer"]));
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdMoneyContext>(options => options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=AdMoney;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddTransient<ISignupUser, SignupUser>();
builder.Services.AddTransient<ILoginUser, LoginUser>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
