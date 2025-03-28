using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class OrderItemManager : CrudManager<OrderItem, OrderItemDto, OrderItemCreateDto, OrderItemDto>, IOrderItemService
{
    public OrderItemManager(IRepository<OrderItem> repository) : base(repository)
    {
    }
}




