using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities;


    public class OrderItem : Entity
    {
        //public OrderItem() 
        //{
        //    TotalPrice = Count * Product.Price;
        //}

        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Count { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; }
    }
