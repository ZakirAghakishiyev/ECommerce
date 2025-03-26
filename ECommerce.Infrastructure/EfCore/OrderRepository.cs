using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.EfCore;

public class OrderRepository : EfCoreRepository<Order>, IOrderRepository
{

    public OrderRepository(AppDbContext context) : base(context)
    {
    }
    public override Order? Get(Expression<Func<Order, bool>> predicate)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .Include(o => o.User)
            .FirstOrDefault(predicate);
        return order;
    }

    public override Order? GetById(int id)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .Include(o => o.User)
            .FirstOrDefault(x=>x.Id==id);
        return order;
    }

    public override List<Order> GetAll(Expression<Func<Order, bool>>? predicate = null, bool asNoTracking = false)
    {
        var orders= _context.Orders
            .Include(x=>x.User)
            .Include(x=>x.Items)
            .ToList();
        return orders;
    }

}

