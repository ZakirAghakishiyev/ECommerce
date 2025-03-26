using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using System.Linq.Expressions;

namespace ECommerce.Application.Services;

public class ProductManager : IProductService
{
    private readonly IProductRepository _repository;

    public ProductManager(IProductRepository repository)
    {
        _repository = repository;
    }

    public void Add(ProductCreateDto createDto)
    {
        var product = new Product
        {
            Name = createDto.Name,
            CategoryId = createDto.CategoryId,
            Price = createDto.Price,
            Description = createDto.Description
        };

        _repository.Add(product);
    }

    public ProductDto Get(Expression<Func<Product, bool>> predicate)
    {
        var product = _repository.Get(predicate);

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = new CategoryDto { Id = product.Category.Id, Name = product.Category.Name },
            Price = product.Price,
            Description = product.Description,
        };

        return productDto;
    }

    public List<ProductDto> GetAll(Expression<Func<Product, bool>>? predicate = null, bool asNoTracking = false)
    {
        var categories = _repository.GetAll(predicate, asNoTracking);

        var productDtoList = new List<ProductDto>();

        foreach (var item in categories)
        {
            productDtoList.Add(new ProductDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = new CategoryDto { Id = item.Category.Id, Name = item.Category.Name },
                Price = item.Price,
                Description = item.Description
            });
        }

        return productDtoList;
    }

    public ProductDto GetById(int id)
    {
        var product = _repository.GetById(id) ?? throw new Exception($"Product with Id {id} not found");

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Category = new CategoryDto { Id = product.Category.Id, Name = product.Category.Name },
            Price = product.Price,
            Description = product.Description,
        };
        return productDto;
    }

    public void Remove(int id)
    {
        var existEntity = _repository.GetById(id) ?? throw new Exception("Not found");
        _repository.Remove(existEntity);
    }

    public void Update(ProductUpdateDto updateDto)
    {
        var product = new Product
        {
            Id = updateDto.Id,
            Name = updateDto.Name,
            CategoryId = updateDto.CategoryId,
            Price = updateDto.Price,
            Description = updateDto.Description
        };

        _repository.Update(product);
    }
}



