namespace BO;

public class OrderForList
{
    int ID { get; set; }    
    OrderStatus Status { get; set; }
    int AmountOfItems { get; set; }
    double TotalPrice { get; set; }
}
