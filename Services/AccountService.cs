using System;
using BookCave.Models.ViewModels;

namespace BookCave.Service
{
    public class AccountService : IAccountService
    {
        public void ProcessLogin(LoginViewModel Login)
        {
            if(string.IsNullOrEmpty(Login.Email))
            {
                throw new Exception("Email is missing!");
            }

            if(string.IsNullOrEmpty(Login.Password))
            {
                throw new Exception("Password is missing!");
            }
        }

        public void ProcessRegister(RegisterViewModel Register)
        {
            if(string.IsNullOrEmpty(Register.Email))
            {
                throw new Exception("Email is missing!");
            }

            if(string.IsNullOrEmpty(Register.Password))
            {
                throw new Exception("Password is missing!");
            }

            if(string.IsNullOrEmpty(Register.FirstName))
            {
                throw new Exception("First name is missing!");
            }

            if(string.IsNullOrEmpty(Register.LastName))
            {
                throw new Exception("Last name is missing!");
            }
        }
    }
}