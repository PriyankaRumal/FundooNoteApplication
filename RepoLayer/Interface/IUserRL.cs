﻿using CommonLayer.Model;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity RegistorUser(UserRegistration userRegistration);
    }
}