namespace BO;

public class OrderForList
{
    int ID { get; set; }    
    OrderStatus Status { get; set; }
    int AmountOfItems { get; set; }
    double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
