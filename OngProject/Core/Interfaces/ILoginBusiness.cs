﻿using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ILoginBusiness
    {
        ResponseLoginDto Login(RequestLoginModelDto login);
        ResponseUserDto GetUserLogged();
    }
}