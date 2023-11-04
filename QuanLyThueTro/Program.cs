using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using QuanLyThueTro.Data;
using QuanLyThueTro.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add dbcontext
builder.Services.AddDbContext<MyDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DBThueTro"));
});



builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];
//mã hóa secretkey
var sk = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication
    (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = false,
            ValidateAudience = false,

            //ký vào token
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(sk),
            ClockSkew = TimeSpan.Zero

        };
    }
    );


//automapper
builder.Services.AddAutoMapper(typeof(Program));
// Đăng ký interface và thực hiện các chức năng của nó trong file
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IExtensionService, ExtensionService>();
builder.Services.AddScoped<IUsers, UserService>();
builder.Services.AddScoped<ITinDang, TinDangService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
