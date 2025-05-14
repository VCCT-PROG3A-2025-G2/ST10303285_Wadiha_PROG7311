using FarmersConnectWebApp.Data;
using FarmersConnectWebApp.Models;
using FarmersConnectWebApp.Repositories.Implementations;
using FarmersConnectWebApp.Repositories.Interfaces;
using FarmersConnectWebApp.Services.Implementations;
using FarmersConnectWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
     .AddRoles<IdentityRole>()
     .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountServices, AccountService>();
builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
builder.Services.AddScoped<IFarmerService, FarmerService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    string[] roles = { "Farmer", "Employee" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // 2. Seed Employee User
    var employeeEmail = "employee@example.com";
    var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
    if (employeeUser == null)
    {
        employeeUser = new IdentityUser
        {
            UserName = employeeEmail,
            Email = employeeEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(employeeUser, "Employee123!"); // Use strong password
        await userManager.AddToRoleAsync(employeeUser, "Employee");

        context.Employees.Add(new Employee
        {
            FullName = "John Employee",
            UserId = employeeUser.Id
        });
        await context.SaveChangesAsync();
    }

    var existingEmployee = await context.Employees.FirstOrDefaultAsync(e => e.UserId == employeeUser.Id);

    // 3. Seed Farmer User
    var farmerEmail = "farmer@example.com";
    var farmerUser = await userManager.FindByEmailAsync(farmerEmail);
    if (farmerUser == null)
    {
        farmerUser = new IdentityUser
        {
            UserName = farmerEmail,
            Email = farmerEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(farmerUser, "Farmer123!");
        await userManager.AddToRoleAsync(farmerUser, "Farmer");

        if (existingEmployee != null)
        {
            context.Farmers.Add(new Farmer
            {
                FullName = "Sarah Farmer",
                UserId = farmerUser.Id,
                EmployeeId = existingEmployee.Id 
            });
        }

        await context.SaveChangesAsync();
    }
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run(); 
