using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add identityServer
builder.Services.AddIdentityServer(o =>
{
	// optionları AddCookie'de çekiyoruz buna gerek yok.
	//o.Authentication.CookieLifetime = TimeSpan.FromHours(1);
	//o.Authentication.CookieSlidingExpiration = false;
	o.Authentication.CookieAuthenticationScheme = "cookie";
})
	.AddServerSideSessions()
	.AddDeveloperSigningCredential();

// auth conf
builder.Services.AddAuthentication(defaultScheme: "cookie")
	.AddCookie("cookie", o =>
	{
		o.Cookie.Name = "demo";
		o.ExpireTimeSpan = TimeSpan.FromHours(8);

		o.LoginPath = "/account/login";
	});



var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//	app.UseExceptionHandler("/Home/Error");
//}
//app.UseStaticFiles();

app.UseRouting();

// auth
app.UseAuthentication();

// yetkilendirme middleware
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
