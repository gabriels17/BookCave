using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;

namespace BookCave.Services
{
    public interface IAccountService
    {
        void ProcessLogin(LoginInputModel Login);
        void ProcessRegister(RegisterInputModel Register);
        void ProcessProfile(ProfileInputModel profile);
    }
}