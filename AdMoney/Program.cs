using AdMoney.Data;
using AdMoney.Repository.Interfaces;
using AdMoney.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<AdMoney.Data.AdMoneyContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DatabaseServer"]));
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdMoneyContext>(options => options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=AdMoney;Integrated Security=True;TrustServerCertificate=True"));

builder.Services.AddTransient<ISignupUser, SignupUser>();
builder.Services.AddTransient<ILoginUser, LoginUser>();
builder.Services.AddTransient<IAdvisorClientData, AdvisorClientData>();
builder.Services.AddTransient<IModels,Models>();
builder.Services.AddTransient<IQuestions,Questions>();
builder.Services.AddTransient<IAdminUser,AdminUser>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option =>
        {
            option.LoginPath = "/Home/Index";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        }) ;   

/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options => {
      options.TokenValidationParameters = new()
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Authentication:Issuer"],
          ValidAudience = builder.Configuration["Authentication:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
          )
      };
  });*/
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
