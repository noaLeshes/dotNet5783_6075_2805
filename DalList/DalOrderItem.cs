using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem oi)
    {
        oi.ID = DataSource._nextOrderItemNumber;//updating the running number
        var o = DataSource.ProductList.Find(x => x?.ID == oi.ID);
        if (o != null)
            throw new DalAlreadyExistsIdException(oi.ID, "OrderItem");
        DataSource.OrderItemList.Add(oi);
        return oi.ID;
    }
    public OrderItem GetById(int id)
    {
        return DataSource.OrderItemList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem");
    }
    public void Update(OrderItem oi)
    {
        var isExist = DataSource.OrderItemList.Find(x => x?.ID == oi.ID);
        if (isExist != null)
        {
            OrderItem? o = DataSource.OrderItemList.Find(x => x?.ID == oi.ID);
            DataSource.OrderItemList.Remove(o);
            DataSource.OrderItemList.Add(oi);//updating the wanted order
            return;
        }
        throw new DalMissingIdException(oi.ID, "OrderItem");
    }
    public void Delete(int id)
    {
        DataSource.OrderItemList.Remove(DataSource.OrderItemList.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem"));
    }
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        if (filter == null)
            return new List<OrderItem?>(DataSource.OrderItemList);
        else
            return new List<OrderItem?>(DataSource.OrderItemList).Where(p => filter(p));
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

