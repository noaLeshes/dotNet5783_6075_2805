using BO;
namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList?> GetOrders();
    Order GetOrderDitailes(int id);
    Order UpdateShipping(int id);
    Order UpdateIfProvided(int id);
    OrderTracking GetOrderTracking(int id);
}
