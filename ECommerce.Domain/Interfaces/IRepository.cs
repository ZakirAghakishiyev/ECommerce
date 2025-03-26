using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    T GetById(int id);
    T Get(Expression<Func<T, bool>> predicate);
    List<T> GetAll(Expression<Func<T, bool>>? predicate, bool asNoTracking);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}