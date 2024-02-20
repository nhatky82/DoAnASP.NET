using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;
using WebAppMVC.Data;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

//var configuration = new ConfigurationBuilder()
//	.SetBasePath(Directory.GetCurrentDirectory())
//	.AddJsonFile("appsettings.json")
//	.Build();

//Sử dụng configuration đã build để đăng ký dịch vụ
//builder.Services.AddSingleton<IConfiguration>(configuration);

// Tiếp tục cấu hình ứng dụng của bạn...



// Add the following line to load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<WebAppMVCContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppMVC")));
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.Cookie.Name = "MyCookie";
	options.IdleTimeout = new TimeSpan(15, 0, 0, 0);
});

builder.Services.AddMvc(options =>
{
	options.FormatterMappings.SetMediaTypeMappingForFormat("mp4", "video/mp4");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "search",
		pattern: "Search",
		defaults: new { controller = "Home", action = "Home" });
});

app.Run();
