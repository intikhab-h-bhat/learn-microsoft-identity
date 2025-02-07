using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Custom.Identity.Membership.Data;
using Custom.Identity.Membership.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CustomIdentityMembershipContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'CustomIdentityMembershipContextConnection' not found.");

builder.Services.AddDbContext<CustomIdentityMembershipContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<CustomIdentityMembershipContext>();
//builder.Services.AddDefaultIdentity<IdentityUser>(options => 
//options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<CustomIdentityMembershipContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
