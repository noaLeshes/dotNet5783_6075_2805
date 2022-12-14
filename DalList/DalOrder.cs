using DalApi;
using DO;
namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        order.ID = DataSource._nextOrderNumber;//updating the running number
        var o = DataSource.ProductList.Find(x => x?.ID == order.ID);
        if (o != null)
            throw new DalAlreadyExistsIdException(order.ID, "Order");
        DataSource.OrderList.Add(order);
        return order.ID;

    }
    public Order GetById(int id)
    {
        return DataSource.OrderList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
    }
    public void Update(Order order)
    {
        var isExist = DataSource.OrderList.Find(x => x?.ID == order.ID);
        if (isExist != null)
        {
            Order? o = DataSource.OrderList.Find(x => x?.ID == order.ID);
            DataSource.OrderList.Remove(o);
            DataSource.OrderList.Add(order);//updating the wanted order
            return;
        }
            throw new DalMissingIdException(order.ID, "Order");
    }
    public void Delete(int id)
    {
        DataSource.OrderList.Remove(DataSource.OrderList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order"));
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (filter == null)
            return new List<Order?>(DataSource.OrderList);
        else
            return new List<Order?>(DataSource.OrderList).Where(p => filter(p));
    }
    public Order GetByFilter(Func<Order?, bool>? filter = null)
    {
        return DataSource.OrderList.Find(x => filter!(x)) ?? throw new DalMissingIdException(-1, "Order");
    }
}



