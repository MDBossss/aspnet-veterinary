using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Veterinary.DAL;
using Veterinary.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<VeterinaryManagerDbContext>(options =>
 options.UseSqlServer(
 builder.Configuration.GetConnectionString("VeterinaryManagerDbContext"),
 opt => opt.MigrationsAssembly("Veterinary.DAL")));

builder.Services.AddDefaultIdentity<AppUser>(
    options =>
        options.SignIn.RequireConfirmedAccount = true
).AddRoles<IdentityRole>().AddEntityFrameworkStores<VeterinaryManagerDbContext>();

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



var SupportedCultures = new[]{
	new CultureInfo("hr"), new CultureInfo("en-US")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("hr"),
	SupportedCultures = SupportedCultures,
	SupportedUICultures = SupportedCultures
});


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();



app.Run();
