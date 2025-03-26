using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class OrderManager : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderManager(IOrderRepository repository)
    {
        _repository = repository;
    }

    public void Add(OrderCreateDto createDto)
    {
        var order = new Order
        {
            UserId = createDto.UserId,
            Status = createDto.Status,
            Items = createDto.Items.Select(x => new OrderItem
            {
                Id = x.Id,
                Product = new Product
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Category = new Category { Id = x.Product.Category.Id, Name = x.Product.Category.Name },
                    Price = x.Product.Price,
                    Description = x.Product.Description
                },
                Count = x.Count
            }).ToList()
        };

        _repository.Add(order);
    }

    public OrderDto Get(Expression<Func<Order, bool>> predicate)
    {   var order = _repository.Get(predicate) ?? throw new Exception("Order not found");
        var orderDto = new OrderDto
        {
            Id= order.Id,
            User = new UserDto
            {
                Id= order.UserId,
                FirstName = order.User.FirstName,
                LastName = order.User.LastName,
                Email = order.User.Email,
                Role = order.User.Role
            },
            Status = order.Status,
            Items = order.Items.Select(x => new OrderItemDto
            {
                Id = x.Id,
                Product = new ProductDto
                {
                    Name = x.Product.Name,
                    Category = new CategoryDto
                    {
                        Id = x.Product.Category.Id,
                        Name = x.Product.Category.Name,
                    },
                    Price = x.Product.Price,
                },
                Count = x.Count
            }).ToList()
        };

        return orderDto;
    }

    public List<OrderDto> GetAll(Expression<Func<Order, bool>>? predicate = null, bool asNoTracking = false)
    {
        var categories = _repository.GetAll(predicate, asNoTracking);

        var orderDtoList = new List<OrderDto>();

        foreach (var item in categories)
        {
            orderDtoList.Add(GetById(item.Id));//new OrderDto
            //{
            //    Id = item.Id,
            //    User = new UserDto
            //    {
            //        Id = item.UserId,
            //        FirstName = item.User.FirstName,
            //        LastName = item.User.LastName,
            //        Email = item.User.Email,
            //        Role = item.User.Role
            //    },
            //    Status = item.Status,
            //    Items = item.Items.Select(x => new OrderItemDto
            //    {
            //        Id = x.Id,
            //        Product = new ProductDto
            //        {
            //            Id = x.Product.Id,
            //            Name = x.Product.Name,
            //            Category = new CategoryDto { Id = x.Product.Category.Id, Name = x.Product.Category.Name },
            //            Price = x.Product.Price,
            //            Description = x.Product.Description
            //        },
            //        Count = x.Count
            //    }).ToList()
            //});
        }

        return orderDtoList;
    }

    public OrderDto GetById(int id)
    {
        var order = _repository.GetById(id);

        //var orderDto = new OrderDto
        //{
        //    Id = order.Id,
        //    User = new UserDto
        //    {
        //        Id = order.UserId,
        //        FirstName = order.User.FirstName,
        //        LastName = order.User.LastName,
        //        Email = order.User.Email,
        //        Role = order.User.Role
        //    },
        //    Status = order.Status,
        //    Items = order.Items.Select(x => new OrderItemDto
        //    {
        //        Id = x.Id,
        //        Product = new ProductDto
        //        {
        //            Id = x.Product.Id,
        //            Name = x.Product.Name,
        //            Category = new CategoryDto { Id = x.Product.Category.Id, Name = x.Product.Category.Name },
        //            Price = x.Product.Price,
        //            Description = x.Product.Description
        //        },
        //        Count = x.Count
        //    }).ToList()
        //};

        return Get(x=>x.Id==order.Id);
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id);

        if (existEntity == null) throw new Exception("Not found");

        _repository.Remove(existEntity);
    }

    public void Update(OrderUpdateDto updateDto)
    {
        var order = new Order
        {
            Id= updateDto.Id,
            Status = updateDto.Status
        };

        _repository.Update(order);
    }
}




