using CVgrupp2Main.DatabasLager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CVgrupp2Main.Models;
using CVgrupp2Main.DatabasLager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration.GetConnectionString("DataContext"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly("CVgrupp2"); // Ange MigrationsAssembly här
            }));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<MeddelandeViewModel>();


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
