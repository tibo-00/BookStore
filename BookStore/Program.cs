using BookStore.Data;
using BookStore.Identity;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageCatalog", policyBuilder => policyBuilder.RequireClaim("CanManageCatalog", "true"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddScoped<IAuthorService, AuthorServices>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

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
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    //Resolve ASP .NET Core Identity with DI help
    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
    if (userManager.FindByEmailAsync("admin@test.be").Result == null)
    {
        //Create new user with Claim
        ApplicationUser adminUser = new ApplicationUser();
        adminUser.UserName = "admin@test.be";
        adminUser.Email = "admin@test.be";
        adminUser.EmailConfirmed = true;
        adminUser.FirstName = "Admin";
        adminUser.LastName = "Test";
        adminUser.BirthDate = DateTime.Now;


        //save user with password
        var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
        if (result.Succeeded)
        {
            //define claims for user
            Claim[] claims = new Claim[] {
                new Claim("CanManageCatalog", "true")};
            //add array of claims to user
            userManager.AddClaimsAsync(adminUser, claims).Wait();
        }
    }
    if (userManager.FindByEmailAsync("user@test.be").Result == null)
    {
        // Create new user without Claim
        ApplicationUser regularUser = new ApplicationUser();
        regularUser.UserName = "user@test.be";
        regularUser.Email = "user@test.be";
        regularUser.EmailConfirmed = true;
        regularUser.FirstName = "User";
        regularUser.LastName = "Test";
        regularUser.BirthDate = DateTime.Now;

        // save user with password
        var result = userManager.CreateAsync(regularUser, "User123!").Result;
        if (result.Succeeded)
        {
            // define claims for user (without CanManageCatalog claim)
            Claim[] claims = new Claim[] {
            new Claim("IsUser", string.Empty)};
            // add array of claims to user
            userManager.AddClaimsAsync(regularUser, claims).Wait();
        }
    }
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
