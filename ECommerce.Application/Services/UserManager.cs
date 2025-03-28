﻿using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class UserManager : CrudManager<User, UserDto, UserCreateDto, UserUpdateDto>, IUserService
{
    public UserManager(IRepository<User> repository) : base(repository)
    {
    }
}



