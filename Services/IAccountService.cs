using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface IAccountService
    {
        void ProcessLogin(LoginViewModel Login);
        void ProcessRegister(RegisterViewModel Register);
    }
}