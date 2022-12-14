﻿namespace DO;

public struct OrderItem
{
    public int ID { get; set; }
    public int? OrderId { get; set; }
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

  
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}


