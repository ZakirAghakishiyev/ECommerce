using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using System.Linq.Expressions;
using static ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Interfaces;

public interface IOrderService: ICrudService<Order, OrderDto, OrderCreateDto, OrderUpdateDto>
{
    //OrderDto GetById(int id);
    //OrderDto Get(Expression<Func<Order, bool>> predicate);
    //List<OrderDto> GetAll(Expression<Func<Order, bool>>? predicate, bool asNoTracking);
    //void Add(OrderCreateDto createDto);
    //void Update(OrderUpdateDto updateDto);
    //void Remove(int id);
}
