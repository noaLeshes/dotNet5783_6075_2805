namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
