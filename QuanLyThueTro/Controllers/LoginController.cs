using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly AppSettings _appSettings;

        public LoginController(MyDBContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("LoginEmployee")]
        public IActionResult Validate(LoginModel model)
        {
            var user = _context.users.SingleOrDefault(p => p.userName == model.userName
            && model.password == p.passwordUser && p.trangThai==true && (p.idChucVu == "NVKD" || p.idChucVu == "Admin"));

            if (user == null)
                return BadRequest();
            return Ok(new
            {
                Success = true,
                Message = "Authentication success",
                Data = GenerateToken(user)
            });
        }
        [HttpPost("LoginGuest")]
        public IActionResult LoginGuest(LoginModel model)
        {
            var user = _context.users.SingleOrDefault(p => p.userName == model.userName
            && model.password == p.passwordUser && p.trangThai == true && (p.idChucVu == "CT" || p.idChucVu == "NT"));

            if (user == null)
                return BadRequest();
            return Ok(new
            {
                Success = true,
                Message = "Authentication success",
                Data = GenerateToken(user)
            });
        }



        private string GenerateToken(Users u)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, u.hoTen),
            new Claim("idUser", u.idUser.ToString())
            }),

                Expires = DateTime.UtcNow.AddMinutes(10),

                // Thêm các roles vào token
                Claims = new Dictionary<string, object>
                    {
                        {"roles", u.idChucVu}
                    },

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);

        }

    }
}
