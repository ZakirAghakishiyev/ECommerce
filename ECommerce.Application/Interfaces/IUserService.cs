﻿using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces;

public interface IUserService
{
    UserDto GetById(int id);
    UserDto Get(Expression<Func<User, bool>> predicate);
    List<UserDto> GetAll(Expression<Func<User, bool>>? predicate, bool asNoTracking);
    void Add(UserCreateDto createDto);
    void Update(UserUpdateDto updateDto);
    void Remove(int id);
}