using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class BasketDto
    {
        public List<BasketItemDto>? BasketItems { get; set; } = new();

    }

    public class BasketItemDto
    {
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }


}
