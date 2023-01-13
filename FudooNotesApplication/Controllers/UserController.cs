using BussinessLayer.Interface;
using BussinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FudooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBl;
        public UserController(IUserBL userBl)
        {
                this.userBl = userBl;
        }
        [HttpPost]
        [Route("UserRegistration")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBl.RegistorUser(userRegistration);
                if(result !=null)
                {
                    return this.Ok(new {success=true , message="Registration Successfull!",data=result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration Unsuccessfull!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UserLogin")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var result = userBl.LoginUser(userLogin);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Login Successfull!", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Login Unsuccessfull!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBl.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Sent Successfully!", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Mail Sent Unsuccessfull!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult PasswordReset(string new_Password,string confirm_Password)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBl.ResetPassword(email,new_Password,confirm_Password);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Password reset Successfull!", data = result });
                }
                else
                {
                    return this.NotFound(new { success = false, message = "Password reset Failed!" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
