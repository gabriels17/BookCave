using BookCave.Models.ViewModels;

namespace BookCave.Service
{
    public interface IAccountService
    {
        void ProcessLogin(LoginViewModel Login);
        void ProcessRegister(RegisterViewModel Register);
    }
}