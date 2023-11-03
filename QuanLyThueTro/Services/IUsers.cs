using QuanLyThueTro.Model;

namespace QuanLyThueTro.Services
{
    public interface IUsers
    {
        public Task<Users> TerminateUser(Users user);
        // khoi phuc va gui mk moi cho mail
        public Task<Users> ResetAccount(Users user);
        public bool ValidatePassword(string password);
        public bool ValidateEmail(string password);
        public bool ValidatePhone(string password);
        public bool ValidateUserName(string userNamess);
        public FileStream GetImageById(string id);
    }
}
