﻿namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? img { get; set; }
    public Category Category { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
