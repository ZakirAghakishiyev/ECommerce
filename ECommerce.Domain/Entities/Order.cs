using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities;

public class Order : Entity
    {
        public Order()
        {
            Time = DateTime.Now;
        }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime? Time { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public Status Status { get; set; }

    }
