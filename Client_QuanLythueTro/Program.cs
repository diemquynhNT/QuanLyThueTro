using Client_QuanLythueTro.APIGateWay;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<APIGateWayTinDang>();
builder.Services.AddScoped<APIGateWayDichVu>();
builder.Services.AddScoped<APIGateWayUsers>();
builder.Services.AddScoped<TinDang_PhongTro_GateWay>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ChuChoThue}/{action=QLLichXemPhong}/{id?}");

app.Run();
