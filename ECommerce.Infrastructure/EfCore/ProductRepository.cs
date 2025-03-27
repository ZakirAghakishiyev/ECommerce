using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Infrastructure.EfCore;

public class ProductRepository : EfCoreRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public override Product Get(Expression<Func<Product, bool>> predicate)
    {
        var product = _context.Products
            .Include(o => o.Category)
            .AsNoTracking()
            .FirstOrDefault(predicate);
        return product;
    }

    public override Product GetById(int id)
    {
        var product = _context.Products
            .Include(o => o.Category)
            .AsNoTracking()
            .FirstOrDefault(x=>x.Id==id);
        return product;
    }

    public override List<Product> GetAll(Expression<Func<Product, bool>>? predicate = null, bool asNoTracking = false)
    {
        // Start with IQueryable
        IQueryable<Product> query = _context.Products
            .Include(x => x.Category)
            .AsNoTracking();

        // Apply predicate if provided
        if (predicate != null)
        {
            query = query.Where(predicate); // Works with IQueryable
        }

        // Apply AsNoTracking if requested
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query.ToList();
    }

}