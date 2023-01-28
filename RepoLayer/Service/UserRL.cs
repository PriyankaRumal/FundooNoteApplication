using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace RepoLayer.Service
{
    public class UserRL: IUserRL
    {
        FundoContext fundo;
        private readonly string _secret;
        private readonly string _expDate;
        public UserRL(FundoContext fundo, IConfiguration config)
        {
            this.fundo = fundo;
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }
        public UserEntity RegistorUser(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName=userRegistration.FirstName;
                userEntity.LastName=userRegistration.LastName;
                userEntity.Email=userRegistration.Email;
               userEntity.Password= EncryptPassword(userRegistration.Password);
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
        public string EncryptPassword(string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    return null;
                }
                else
                {
                    byte[] strongpassword = ASCIIEncoding.UTF8.GetBytes(Password);
                    string EncryptPassword = Convert.ToBase64String(strongpassword);
                    return EncryptPassword;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DecryptPassword(string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Password))
                {
                    return null;
                }
                else
                {
                    byte[] encryptpassword = Convert.FromBase64String(Password);
                    string decryptpassword = ASCIIEncoding.ASCII.GetString(encryptpassword);
                    return decryptpassword;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public string LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = fundo.UserTable.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                if (result != null)
                {
                    string decrypass = DecryptPassword(result.Password);
                    if (decrypass == userLogin.Password)
                    {
                        var token = GenerateSecurityToken(result.Email, result.UserId);
                        return token;
                    }
                    return "login failed";
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
        public string GenerateSecurityToken(string email,long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var result = fundo.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if(result!=null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    MSMQModel mq = new MSMQModel();
                    mq.sendData2Queue(token);
                    return token;
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
       public bool ResetPassword(string email,string new_Password,string confirm_Password)
        {
            try
            {
                if(new_Password==confirm_Password)
                {
                    var result = fundo.UserTable.Where(x => x.Email == email).FirstOrDefault();
                    string newEncryptPass = EncryptPassword(new_Password);
                    result.Password = new_Password;
                    fundo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
