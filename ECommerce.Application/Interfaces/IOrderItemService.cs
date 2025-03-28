using ECommerce.Application.DTOs;
using System.Linq.Expressions;
using ECommerce.Domain.Entities;


namespace ECommerce.Application.Interfaces;

public interface IOrderItemService: ICrudService<OrderItem, OrderItemDto, OrderItemCreateDto, OrderItemDto>
{
    //OrderItemDto GetById(int id);
    //OrderItemDto Get(Expression<Func<OrderItem, bool>> predicate);
    //List<OrderItemDto> GetAll(Expression<Func<OrderItem, bool>>? predicate, bool asNoTracking);
    //void Add(OrderItemCreateDto createDto);
    //void Update(OrderItemDto updateDto);
    //void Remove(int id);
}