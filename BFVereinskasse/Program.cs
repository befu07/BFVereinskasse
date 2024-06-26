using BFVereinskasse.Data;
using BFVereinskasse.Services;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BfvereinskasseContext>();
builder.Services.AddTransient<PaymentService>();
builder.Services.AddTransient<MemberService>();

var app = builder.Build();

var cultureInfo = new CultureInfo("de-DE");
cultureInfo.NumberFormat.CurrencySymbol = "Heislbesn";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
