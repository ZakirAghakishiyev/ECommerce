using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class OrderItemManager : IOrderItemService
{
    private readonly IOrderItemRepository _repository;

    public OrderItemManager(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    public void Add(OrderItemCreateDto createDto)
    {

        var orderItem = new OrderItem
        {
            OrderId = createDto.OrderId,
            ProductId = createDto.ProductId,
            Count = createDto.Count
        };

        _repository.Add(orderItem);
    }

    public OrderItemDto Get(Expression<Func<OrderItem, bool>> predicate)
    {
        var orderItem = _repository.Get(predicate);

        var orderItemDto = new OrderItemDto
        {
            Id = orderItem.Id,
            Product = new ProductDto
            {
                Name = orderItem.Product.Name,
                Category = new CategoryDto
                {
                    Id = orderItem.Product.Category.Id,
                    Name = orderItem.Product.Category.Name,
                },
                Price = orderItem.Product.Price,
            },
            Count = orderItem.Count
        };

        return orderItemDto;
    }

    public List<OrderItemDto> GetAll(Expression<Func<OrderItem, bool>>? predicate = null, bool asNoTracking = false)
    {
        var categories = _repository.GetAll(predicate, asNoTracking);

        var orderItemDtoList = new List<OrderItemDto>();

        foreach (var item in categories)
        {
            orderItemDtoList.Add(GetById(item.Id)/*new OrderItemDto
            {
                Id = item.Id,
                Product = new ProductDto
                {
                    Name = item.Product.Name,
                    Category = new CategoryDto
                    {
                        Id = item.Product.Category.Id,
                        Name = item.Product.Category.Name,
                    },
                    Price = item.Product.Price,
                },
                Count = item.Count
            }*/);
        }

        return orderItemDtoList;
    }

    public OrderItemDto GetById(int id)
    {
        return Get(x=>x.Id==id);
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id);

        if (existEntity == null) throw new Exception("Not found");

        _repository.Remove(existEntity);
    }

    public void Update(OrderItemDto updateDto)
    {
        var orderItem = new OrderItem
        {
            Id = updateDto.Id,
            OrderId = updateDto.OrderId,
            ProductId = updateDto.Product.Id,
            Count = updateDto.Count
        };

        _repository.Update(orderItem);
    }
}



