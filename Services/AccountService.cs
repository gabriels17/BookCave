using System;
using BookCave.Models.ViewModels;
using BookCave.Models;
using BookCave.Repositories;
using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public class AccountService : IAccountService
    {
        public void ProcessLogin(LoginInputModel Login)
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

        public void ProcessProfile(ProfileInputModel profile)
        {
            if(string.IsNullOrEmpty(profile.FirstName))
            {
                throw new Exception("First name is missing");
            }

            if(string.IsNullOrEmpty(profile.LastName))
            {
                throw new Exception("Last name is missing");
            }
            if(string.IsNullOrEmpty(profile.FullName))
            {
                throw new Exception("Name is missing");
            }

            if(string.IsNullOrEmpty(profile.ShippingAddress))
            {
                throw new Exception("Address is missing");
            }

            if(string.IsNullOrEmpty(profile.City))
            {
                throw new Exception("City is missing");
            }

            if(string.IsNullOrEmpty(profile.State))
            {
                throw new Exception("State is missing");
            }

            if(string.IsNullOrEmpty(profile.Postcode))
            {
                throw new Exception("Postcode is missing");
            }

            if(string.IsNullOrEmpty(profile.Country))
            {
                throw new Exception("Country is missing");
            }
        }

        public void ProcessRegister(RegisterInputModel Register)
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