using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();//ekledik

//claimler i�in
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt=>
{
    opt.LoginPath = "/Account/Login"; //giri� i�in y�nlendirme
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    opt.SlidingExpiration = true; //s�re �telensin mi
    opt.AccessDeniedPath = "/Home/Eri�imEngeli"; //yetkisi olmayanlar�n g�recegi sayfa
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();//ekledik
app.UseAuthorization();

app.MapControllerRoute
    (
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
