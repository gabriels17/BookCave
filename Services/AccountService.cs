using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class AccountService
    {
        private AccountRepo _accountRepo;

        public AccountService()
        {
            _accountRepo = new AccountRepo();
        }

        public ProfileViewModel GetProfile(ApplicationUser user)
        {
            var account = new ProfileViewModel {Name = user.UserName, Email = user.Email};
            return account;
        }
    }
}