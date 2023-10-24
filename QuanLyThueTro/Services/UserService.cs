using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace QuanLyThueTro.Services
{
    public class UserService:IUsers
    {
        private MyDBContext _context;
        private GenerateAlphanumericId getId;

        public UserService(MyDBContext context)
        {
            _context = context;
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

        public async Task<Users> ResetAccount(Users user)
        {
            //user.statusUser = true;
            //var newPassword = generateId.GenerateId(5);
            //user.passWord = newPassword;

            //var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Contact", "contactcentersgt@gmail.com"));
            //message.To.Add(new MailboxAddress("Tên người nhận", user.emailAddress));
            //message.Subject = "Reset Account";
            //message.Body = new TextPart("plain")
            //{
            //    Text = "new password: " + newPassword
            //};

            //// Cấu hình SmtpClient
            //using (var smtpClient = new SmtpClient())
            //{
            //    smtpClient.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            //    smtpClient.Authenticate("contactcentersgt@gmail.com", "akoi wjtt rpzd xpmw");

            //    // Gửi email
            //    smtpClient.Send(message);

            //    // Đóng kết nối
            //    smtpClient.Disconnect(true);
            //    _context.SaveChanges();
                return user;
            }
        

        public async Task<Users> TerminateUser(Users user)
        {
            user.trangThai = false;
            _context.SaveChanges();
            return user;
        }

    }
}
