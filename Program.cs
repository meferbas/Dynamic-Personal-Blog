using Microsoft.EntityFrameworkCore;
using BlogApp.DAL.Context;
using BlogApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using BlogApp.Services;	

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<MyBlogContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDatabase")));

builder.Services.AddIdentity<User, IdentityRole>()
	//... (identity ayarlarý)
	.AddEntityFrameworkStores<MyBlogContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<User>>();
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	await DataSeeder.SeedSuperAdminAsync(userManager, roleManager); // Seed the data
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
