using DalApi;
using DO;
namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        order.ID = DataSource._nextOrderNumber;//updating the running number
        bool isExist = DataSource.OrderList.Contains(order);
        if (isExist)
        {
            throw new DalAlreadyExistsIdException(order.ID, "Order");
        }
        DataSource.OrderList.Add(order);
        return order.ID;

    }
    public Order GetById(int id)
    {
        return DataSource.OrderList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
    }
    public void Update(Order order)
    {
        bool isExist = DataSource.OrderList.Contains(order);
        if (isExist)
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
        Order o = DataSource.OrderList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
        DataSource.OrderList.Remove(o);
    }
    public IEnumerable<Order?> GetAll()
    {
        return new List<Order?>(DataSource.OrderList);
    }
}



