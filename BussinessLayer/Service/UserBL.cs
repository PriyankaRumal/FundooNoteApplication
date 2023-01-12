using BussinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL userRl;
        public UserBL(IUserRL userRl)
        {
            this.userRl = userRl;
        }
    
        public UserEntity RegistorUser(UserRegistration userRegistration)
        {
            try
            {
                return userRl.RegistorUser(userRegistration);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public  string LoginUser(UserLogin userLogin)
        {
            try
            {
                return userRl.LoginUser(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string ForgetPassword(string email)
        {
            try
            {

                return userRl.ForgetPassword(email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
