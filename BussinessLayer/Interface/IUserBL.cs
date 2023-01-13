using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity RegistorUser(UserRegistration userRegistration);
        public string LoginUser(UserLogin userLogin);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string new_Password, string confirm_Password);
    }
}
