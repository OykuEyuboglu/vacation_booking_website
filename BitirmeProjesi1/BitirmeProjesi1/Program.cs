using BitirmeProjesi1.Data.Context;
using BitirmeProjesi1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;


// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IUserService, UserService>();

// Servis eklemeleri burada yapılır
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService, UserService>(); // UserService'ı kaydediyoruz



// Authentication ve Authorization servisleri ekleniyor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });



//options.LoginPath = "/Account/Login";:
//Eğer bir kullanıcı korumalı bir alana erişmeye çalıştığında kimliği doğrulanmamışsa, kullanıcıyı bu yola (örneğin, /Account/Login) yönlendirecektir.

//options.AccessDeniedPath = "/Account/AccessDenied";:
//Kullanıcı, gerekli izinlere sahip değilse (örneğin, sadece adminlerin erişebileceği bir sayfaya erişmeye çalıştığında) bu yola (örneğin, /Account/AccessDenied) yönlendirilir.


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddDbContext<ProjectContext>();

builder.Services.AddScoped<IHotelService, HotelService>();




var app = builder.Build();

// Middleware yapılandırmaları burada yapılır
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization middleware'leri
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();