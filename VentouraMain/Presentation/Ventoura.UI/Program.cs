using Ventoura.Persistence.ServiceRegistration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ventoura.Domain.Entities;
using Ventoura.Application.Abstractions.Services;
using Ventoura.Persistence.Implementations.Services;
using Ventoura.UI.MiddleWares;
using Ventoura.Persistence.DAL;
using Stripe;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
    options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
});
// Add services to the container.
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddControllersWithViews();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService,MailService>();

var app = builder.Build();
//app.UseMiddleware<GlobalExceptionHandlerMiddleWare>();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCookiePolicy(new CookiePolicyOptions()
{
	MinimumSameSitePolicy = SameSiteMode.None
});
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
