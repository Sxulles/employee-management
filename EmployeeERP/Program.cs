using AutoMapper;
using EmployeeERP.Data;
using EmployeeERP.Models;
using EmployeeERP.Models.DbEntities;
using EmployeeERP.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));
AddServices();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeERPConnectionString"));
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

await AddRoles();

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

















void AddServices()
{
    builder.Services
        .AddIdentityCore<Employee>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

}

async Task AddRoles()
{
    using var scope = app.Services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    var tAdmin = CreateAdminRole(roleManager);
    await tAdmin;

    var tUser = CreateUserRole(roleManager);
    await tUser;
}

async Task CreateAdminRole(RoleManager<IdentityRole<Guid>> roleManager)
{
    await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
}

async Task CreateUserRole(RoleManager<IdentityRole<Guid>> roleManager)
{
    await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
}
