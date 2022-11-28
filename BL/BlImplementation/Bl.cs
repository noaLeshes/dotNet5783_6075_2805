using BlApi;

namespace BlImplementation;

public class Bl : IBl
{
    public Bl() { }
    public IOrder Order { get; set; } = new Order();
    public IOrder Product { get; set; } = new Product();
    public IOrder Cart { get; set; } = new Cart();

}
