using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class OrderManager : CrudManager<Order, OrderDto, OrderCreateDto, OrderUpdateDto>, IOrderService
{
    public OrderManager(IRepository<Order> repository) : base(repository)
    {
    }
}




