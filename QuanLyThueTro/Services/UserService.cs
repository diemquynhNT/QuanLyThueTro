using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using System;

namespace QuanLyThueTro.Services
{
    public class UserService : IUsers
    {
        private MyDBContext _context;
        private GenerateAlphanumericId getId;

        public UserService(MyDBContext context)
        {
            _context = context;
            getId = new GenerateAlphanumericId();
        }
        public bool ValidatePassword(string password)
        {
            string passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).{8,}$";
            bool isValid = Regex.IsMatch(password, passwordRegex);
            return isValid;
        }

        public bool ValidateEmail(string email)
        {
            var emailUser = _context.users.Where(t => t.emailUser == email).FirstOrDefault();
            if (emailUser == null)
                return true;
            return false;
        }

        public bool ValidatePhone(string phones)
        {
            var p = _context.users.Where(t => t.sdtUsers == phones).FirstOrDefault();
            if (p == null)
                return true;
            return false;
        }
        public bool ValidateUserName(string users)
        {
            var p = _context.users.Where(t => t.userName == users).FirstOrDefault();
            if (p == null)
                return true;
            return false;
        }

        public bool ResetAccount(String id)
        {
            var user = _context.users.Where(t => t.idUser == id).FirstOrDefault();
            if (user == null)
                return false;

                 user.trangThai = true;
                var newPassword = getId.GenerateId(8);
                user.passwordUser = newPassword;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Contact", "contactcentersgt@gmail.com"));
                message.To.Add(new MailboxAddress("Tên người nhận", user.emailUser));
                message.Subject = "Reset Account";
                message.Body = new TextPart("plain")
                {
                    Text = "Mật khẩu đăng nhập mới của user: "+user.userName+" là: " + newPassword 
                };

                // Cấu hình SmtpClient
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtpClient.Authenticate("contactcentersgt@gmail.com", "akoi wjtt rpzd xpmw");

                    // Gửi email
                    smtpClient.Send(message);

                    // Đóng kết nối
                    smtpClient.Disconnect(true);
                  
                }
                _context.SaveChanges();
                return true;


        }
        

        public async Task<Users> TerminateUser(Users user)
        {
            user.trangThai = false;
            _context.SaveChanges();
            return user;
        }
        public FileStream GetImageById(string id)
        {
            Users users = _context.users.SingleOrDefault(t => t.idUser == id);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            var filePath = Path.Combine(path, users.hinhAnh);
            if (System.IO.File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return fileStream;
            }
            return null;

        }

        public bool QuenMatKhau(string email)
        {
            var user = _context.users.Where(t => t.emailUser == email).FirstOrDefault();
            if (user == null)
                return false;

            var newPassword = getId.GenerateId(8);
            user.passwordUser = newPassword;
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Thông báo", "contactcentersgt@gmail.com"));
            message.To.Add(new MailboxAddress("Tên người nhận", user.emailUser));
            message.Subject = "Quên mật khẩu";
            message.Body = new TextPart("plain")
            {
                Text = "Mật khẩu đăng nhập mới của user: " + user.userName + " là: " + newPassword
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtpClient.Authenticate("contactcentersgt@gmail.com", "akoi wjtt rpzd xpmw");
                smtpClient.Send(message);
                smtpClient.Disconnect(true);
            }
            _context.SaveChanges();
            return true;
        }
    }
}
