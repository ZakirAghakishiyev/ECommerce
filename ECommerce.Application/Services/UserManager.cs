using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class UserManager : IUserService
{
    private readonly IUserRepository _repository;

    public UserManager(IUserRepository repository)
    {
        _repository = repository;
    }

    public void Add(UserCreateDto createDto)
    {
        var user = new User
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            Role=createDto.Role,
            Email=createDto.Email,
        };

        _repository.Add(user);
    }

    public UserDto? Get(Expression<Func<User, bool>> predicate)
    {
        var user = _repository.Get(predicate);

        var userDto = new UserDto
        {
            Id=user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Orders=user.Orders,
            Role = user.Role,
            Email = user.Email,
        };

        return userDto;
    }

    public List<UserDto>? GetAll(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false)
    {
        var categories = _repository.GetAll(predicate, asNoTracking);

        var userDtoList = new List<UserDto>();

        foreach (var item in categories)
        {
            userDtoList.Add(new UserDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Orders = item.Orders,
                Role = item.Role,
                Email = item.Email,
            });
        }

        return userDtoList;
    }

    public UserDto? GetById(int id)
    {
        var user = _repository.GetById(id);

        var userDto = new UserDto
        {
            Id= user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Orders = user.Orders,
            Role = user.Role,
            Email = user.Email,
        };

        return userDto;
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id);

        if (existEntity == null) throw new Exception("Not found");

        _repository.Remove(existEntity);
    }

    public void Update(UserUpdateDto updateDto)
    {
        var user = new User
        {
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            Password= updateDto.Password,
            Email = updateDto.Email,
        };

        _repository.Update(user);
    }
}



