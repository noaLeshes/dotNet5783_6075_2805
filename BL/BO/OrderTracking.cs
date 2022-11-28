namespace BO;

public class OrderTracking
{
    public int ID { get; set; } 
    public OrderStatus Status { get; set; }
    public List< Tuple<DateTime,string> >? Tracking { get; set; }            
}
