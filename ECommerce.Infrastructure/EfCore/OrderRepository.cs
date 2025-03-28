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
}

