using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {

        public int Id { get; set; }
        public UserDto? User { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }
        public decimal TotalPrice {  get; set; }
        public List<OrderItemDto> Items { get; set; } = [];

    }

    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderItemCreateDto> Items { get; set; } = [];
        public Status Status { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
