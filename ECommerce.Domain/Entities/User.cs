﻿namespace ECommerce.Domain.Entities;

public class User : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Roles Role { get; set; }

    public List<Order>? Orders { get; set; } = new();
}
