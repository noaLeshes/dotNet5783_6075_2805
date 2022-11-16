using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem oi)
    {
        if (DataSource.Config.arrOrderItemIndex == 200)
            throw new Exception("The array is full");
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == oi.ID)
                throw new Exception("The orderItem is already existing");
        }
        DataSource.OrderItemArr[DataSource.Config.arrOrderItemIndex] = oi;
        return oi.ID;
    }
    public OrderItem GetById(int id)
    {

        OrderItem d;
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            d = DataSource.OrderItemArr[i];
            if (d.ID == id)
                return d;
        }
        throw new Exception("not found");
    }
    public void Update(OrderItem oi)
    {

        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == oi.ID)
                DataSource.OrderItemArr[i] = oi;
        }
        throw new Exception("not found");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == id)
            {
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[DataSource.Config.arrOrderItemIndex];
               // DataSource.OrderArr[DataSource.Config.arrOrderIndex]=0;
                DataSource.Config.arrOrderItemIndex--;
            }
        }
    }
    public OrderItem[] GetAll()
    {
        OrderItem[] newArr = new OrderItem[DataSource.Config.arrOrderItemIndex];
        for(int i = 0; i< DataSource.Config.arrOrderItemIndex; i++)
        {
            newArr[i] = DataSource.OrderItemArr[i];
        }
        return newArr;
    }
    public OrderItem[] GetAllOrderProducts(int orderId)
    {
        OrderItem[] newArr = new OrderItem[DataSource.Config.arrOrderItemIndex];
        OrderItem p = new OrderItem() ;
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            int j = 0;
            if (DataSource.OrderItemArr[i].OrderId == orderId)
            {
                p = DataSource.OrderItemArr[i];
                newArr[j] = p;
                j++;
            }
        }
        return newArr;
    }
    public OrderItem GetByProductIdAndOrderId(int orderId, int productId)
    {
        OrderItem p = new OrderItem();
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].OrderId == orderId)
            {
                if (DataSource.OrderItemArr[i].ProductId == productId)
                {
                    p = DataSource.OrderItemArr[i];
                    return p;
                }
            }
        }
        throw new Exception("The item doesn't exist");
    }

}


