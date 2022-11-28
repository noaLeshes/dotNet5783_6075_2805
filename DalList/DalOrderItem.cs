using DalApi;
using DO;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem oi)
    {
        oi.ID = DataSource._nextOrderItemNumber;//updating the running number
        bool isExist = DataSource.OrderItemList.Contains(oi);
        if (isExist)
        {
            throw new Exception("not found");
        }
        DataSource.OrderItemList.Add(oi);
        return oi.ID;
    }
    public OrderItem GetById(int id)
    {
        return DataSource.OrderItemList.Find(x => x?.ID == id) ?? throw new Exception("not found");
    }
    public void Update(OrderItem oi)
    {
        bool isExist = DataSource.OrderItemList.Contains(oi);
        if (isExist)
        {
            OrderItem? o = DataSource.OrderItemList.Find(x => x?.ID == oi.ID);
            DataSource.OrderItemList.Remove(o);
            DataSource.OrderItemList.Add(oi);//updating the wanted order
            return;
        }
        throw new Exception("not found");
    }
    public void Delete(int id)
    {
        OrderItem oi = DataSource.OrderItemList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        DataSource.OrderItemList.Remove(oi);
    }
    public IEnumerable<OrderItem?> GetAll()
    {
        return new List<OrderItem?>(DataSource.OrderItemList);
    }
    public IEnumerable<OrderItem?> GetAllOrderProducts(int orderId)
    {
        List<OrderItem?> newList = new List<OrderItem?>();
        foreach (var oi in DataSource.OrderItemList)
        {
            if (oi?.OrderId == orderId)
            {
                newList.Add(oi);
            }
        }
        return newList;
    }
    public OrderItem GetByProductIdAndOrderId(int orderId, int productId)
    {
        return DataSource.OrderItemList.Find(x => x?.OrderId == orderId && x?.ProductId == productId) ?? throw new Exception("The item doesn't exist");
    }

}

