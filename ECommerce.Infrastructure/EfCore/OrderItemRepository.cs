using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.EfCore;

public class OrderItemRepository : EfCoreRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(AppDbContext context) : base(context)
    {
    }

    public override OrderItem? Get(Expression<Func<OrderItem, bool>> predicate)
    {
        var order = _context.OrderItems
            .AsNoTracking()
            .Include(o => o.Product)
            .ThenInclude(o=>o.Category)
            .Include(o => o.Order)
            .FirstOrDefault(predicate);
        return order;
    }

    public override OrderItem? GetById(int id)
    {
        var order = _context.OrderItems
            .AsNoTracking()
            .Include(o => o.Product)
            .Include(o => o.Order)
            .FirstOrDefault(x => x.Id == id);
        return order;
    }

    public override List<OrderItem> GetAll(Expression<Func<OrderItem, bool>>? predicate = null, bool asNoTracking = false)
    {
        var orderItems = _context.OrderItems
             .AsNoTracking()
             .Include(o => o.Product)
             .Include(o => o.Order)
             .ToList();
        return orderItems;
    }
}
