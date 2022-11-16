using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem oi)
    {
        if (DataSource.Config.arrOrderItemIndex == 200)
            throw new Exception("The array is full");//if the array is full throw
        oi.ID = DataSource.Config.NextOrderItemNumber;//updating the running number
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == oi.ID)//if already exsist
            {
                throw new Exception("The orderItem is already existing");
            }
        }
        DataSource.OrderItemArr[DataSource.Config.arrOrderItemIndex++] = oi;
        return oi.ID;
       
    }
    public OrderItem GetById(int id)
    {
        OrderItem d;
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            d = DataSource.OrderItemArr[i];
            if (d.ID == id)//serch the array to find the wanted id
                return d;
        }
        throw new Exception("not found");
    }
    public void Update(OrderItem oi)
    {

        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == oi.ID)
            {
                DataSource.OrderItemArr[i] = oi;//updating the wanted orderItem
                return;
            }
        }
        throw new Exception("not found");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == id)
            {//איזה בדיקת תקינות צריך לבצע
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[DataSource.Config.arrOrderItemIndex];
                //taking the last orderItem and replacing it with what we want to delete
                DataSource.Config.arrOrderItemIndex--;//reducing the index to delete orderItem
                return;
            }
        }
        throw new Exception("not found");//if not found
    }
    public OrderItem[] GetAll()
    {
        OrderItem[] newArr = new OrderItem[DataSource.Config.arrOrderItemIndex];
        for(int i = 0; i< DataSource.Config.arrOrderItemIndex; i++)
        {
            newArr[i] = DataSource.OrderItemArr[i];//copy all the orderItems to the new array
        }
        return newArr;
    }
    public OrderItem[] GetAllOrderProducts(int orderId)
    {
        OrderItem[] newArr = new OrderItem[4];//there is limit of up to 4 produts in order
        OrderItem p = new OrderItem();
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            int j = 0;
            if (DataSource.OrderItemArr[i].OrderId == orderId)//search for a certain order
            {
                p = DataSource.OrderItemArr[i];
                newArr[j] = p;//add the orderItem that belongs to the order to the array
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
            if (DataSource.OrderItemArr[i].OrderId == orderId)//search for order
            {
                if (DataSource.OrderItemArr[i].ProductId == productId)//search for product
                {
                    p = DataSource.OrderItemArr[i];//return orderItem
                    return p;
                }
            }
        }
        throw new Exception("The item doesn't exist");
    }

}


