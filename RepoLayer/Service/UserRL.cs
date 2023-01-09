using CommonLayer.Model;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RepoLayer.Service
{
    public class UserRL: IUserRL
    {
        FundoContext fundo;
        public UserRL(FundoContext fundo)
        {
            this.fundo = fundo;
        }
        public UserEntity RegistorUser(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName=userRegistration.FirstName;
                userEntity.LastName=userRegistration.LastName;
                userEntity.Email=userRegistration.Email;
                userEntity.Password=userRegistration.Password;
                fundo.UserTable.Add(userEntity);
                int result = fundo.SaveChanges();
                if(result >0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
