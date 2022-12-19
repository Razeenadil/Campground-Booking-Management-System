using CampgroundBooking.Areas.Identity;
using CampgroundBooking.Backend.CampgroundUtils;
using CampgroundBooking.Backend.NewEmployeeUtlis;
using CampgroundBooking.Backend.CampsiteUtils;
using CampgroundBooking.Backend.Interfaces;
using CampgroundBooking.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CampgroundBooking.Backend.Data;
using CampgroundBooking.Backend.Data.CampgroundData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("LoginDB");

// Not sure why the following line is needed to run the DB
builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//connecting to our DB
builder.Services.AddTransient<ISqlDataAcceess, SqlDataAcceess>();
builder.Services.AddTransient<IPersonData, PersonData>();
builder.Services.AddTransient<ICampgroundLocationsData, CampgroundLocationsData>();
builder.Services.AddTransient<ICampgroundSitesData, CampgroundSitesData>();
builder.Services.AddTransient<ICampgroundServiceData, CampgroundServiceData>();
builder.Services.AddTransient<ICampgroundOpenDuringData, CampgroundOpenDuringData>();
builder.Services.AddTransient<ICampgroundAmenitiesData, CampgroundAmenitiesData>();
builder.Services.AddTransient<ICampgroundReviewData, CampgroundReviewData>();
builder.Services.AddTransient<ICustomerBookingData, CustomerBookingData>();
builder.Services.AddTransient<IEmployeeData, EmployeeData>();


builder.Services.AddTransient<CampsiteDBServices>();
builder.Services.AddTransient<CampgroundDBServices>();
builder.Services.AddTransient<NewEmployeeDBServices>();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
