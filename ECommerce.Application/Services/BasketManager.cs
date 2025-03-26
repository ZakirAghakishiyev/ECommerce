using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerce.Application.Services
{
    public class BasketManager : IBasketService
    {
        private readonly List<BasketDto> _baskets = new(); 

        public BasketDto GetById(int id)
        {
            return _baskets.FirstOrDefault(b => b.GetHashCode() == id) ?? new BasketDto();
        }

        public BasketDto Get(Expression<Func<BasketDto, bool>> predicate)
        {
            return _baskets.AsQueryable().FirstOrDefault(predicate) ?? new BasketDto();
        }

        public List<BasketDto> GetAll(Expression<Func<BasketDto, bool>>? predicate, bool asNoTracking)
        {
            return predicate == null ? _baskets.ToList() : _baskets.AsQueryable().Where(predicate).ToList();
        }

        public void Add(BasketDto createDto)
        {
            if (createDto != null)
            {
                _baskets.Add(createDto);
            }
        }

        public void Update(BasketDto updateDto)
        {
            var existingBasket = _baskets.FirstOrDefault(b => b.GetHashCode() == updateDto.GetHashCode());
            if (existingBasket != null)
            {
                _baskets.Remove(existingBasket);
                _baskets.Add(updateDto);
            }
        }

        public void Remove(int id)
        {
            var basket = _baskets.FirstOrDefault(b => b.GetHashCode() == id);
            if (basket != null)
            {
                _baskets.Remove(basket);
            }
        }
    }
}
