using Client_QuanLythueTro.APIGateWay;
using Client_QuanLythueTro.Models;
using Client_QuanLythueTro.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SmartBreadcrumbs.Extensions;
using System.Reflection;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<APIGateWayTinDang>();
builder.Services.AddScoped<APIGateWayDichVu>();
builder.Services.AddScoped<APIGateWayUsers>();
builder.Services.AddScoped<TinDang_PhongTro_GateWay>();
builder.Services.AddScoped<LichXemPhong_GateWay>();
builder.Services.AddScoped<APIGateWayKhuVuc>();
builder.Services.AddScoped<GiaoDich_Gateway>();

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<APIGateWayLichXemPhong>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseSession();

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
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
