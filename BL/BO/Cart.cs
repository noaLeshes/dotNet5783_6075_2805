﻿namespace BO;

public class Cart
{
    public string? CostomerName { get; set; }
    public string? CostomerEmail { get; set; }
    public string? CostomerAddress { get; set; }  
    public IEnumerable<OrderItem>? Items { get; set; } 
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
