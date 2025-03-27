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
            .ThenInclude(o => o.Product).ThenInclude(o => o.Category)
            .Include(o => o.User)
            .AsNoTracking()
            .FirstOrDefault(predicate);
        return order;
    }

    public override Order? GetById(int id)
    {
        var order = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(o => o.Product).ThenInclude(o => o.Category)
            .Include(o => o.User)
            .AsNoTracking()
            .FirstOrDefault(x=>x.Id==id);
        return order;
    }

    public override List<Order> GetAll(Expression<Func<Order, bool>>? predicate = null, bool asNoTracking = false)
    {
        var orders= _context.Orders
            .Include(x=>x.User)
            .Include(x=>x.Items)
            .ThenInclude(o => o.Product).ThenInclude(o=>o.Category)
            .AsNoTracking()
            .ToList();
        return orders;
    }

}

