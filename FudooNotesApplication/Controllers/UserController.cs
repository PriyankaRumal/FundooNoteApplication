﻿using BussinessLayer.Interface;
using BussinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}