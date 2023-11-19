using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface IUsers
    {
        public Task<Users> TerminateUser(Users user);
        // khoi phuc va gui mk moi cho mail
        public bool ResetAccount(String id);
        public bool QuenMatKhau(String email);
        public bool ValidatePassword(string password);
        public bool ValidateEmail(string password);
        public bool ValidatePhone(string password);
        public bool ValidateUserName(string userNamess);
        public FileStream GetImageById(string id);
    }
}
