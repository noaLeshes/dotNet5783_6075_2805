namespace DalApi;
using DO;

public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem?> GetAllOrderProducts(int orderId);
    public OrderItem GetByProductIdAndOrderId(int orderId, int productId);
}
