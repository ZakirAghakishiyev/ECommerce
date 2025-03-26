using ECommerce.Application.DTOs;
using System.Linq.Expressions;
using static ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Interfaces;

public interface IBasketService
{
    BasketDto GetById(int id);
    BasketDto Get(Expression<Func<BasketDto, bool>> predicate);
    List<BasketDto> GetAll(Expression<Func<BasketDto, bool>>? predicate, bool asNoTracking);
    void Add(BasketDto createDto);
    void Update(BasketDto updateDto);
    void Remove(int id);
}
