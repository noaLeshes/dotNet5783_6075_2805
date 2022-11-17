using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem oi)
    {
        if (DataSource.arrOrderItemIndex == 200)
            throw new Exception("The array is full");//if the array is full throw
        oi.ID = DataSource._nextOrderItemNumber;//updating the running number
        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            if (DataSource.orderItemArr[i].ID == oi.ID)//if already exsist
            {
                throw new Exception("The orderItem is already existing");
            }
        }
        DataSource.orderItemArr[DataSource.arrOrderItemIndex++] = oi;
        return oi.ID;
       
    }
    public OrderItem GetById(int id)
    {
        OrderItem d;
        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            d = DataSource.orderItemArr[i];
            if (d.ID == id)//serch the array to find the wanted id
                return d;
        }
        throw new Exception("not found");
    }
    public void Update(OrderItem oi)
    {

        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            if (DataSource.orderItemArr[i].ID == oi.ID)
            {
                DataSource.orderItemArr[i] = oi;//updating the wanted orderItem
                return;
            }
        }
        throw new Exception("not found");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            if (DataSource.orderItemArr[i].ID == id)
            {
                DataSource.orderItemArr[i] = DataSource.orderItemArr[DataSource.arrOrderItemIndex];
                //taking the last orderItem and replacing it with what we want to delete
                DataSource.arrOrderItemIndex--;//reducing the index to delete orderItem
                return;
            }
        }
        throw new Exception("not found");//if not found
    }
    public OrderItem[] GetAll()
    {
        OrderItem[] newArr = new OrderItem[DataSource.arrOrderItemIndex];
        for(int i = 0; i< DataSource.arrOrderItemIndex; i++)
        {
            newArr[i] = DataSource.orderItemArr[i];//copy all the orderItems to the new array
        }
        return newArr;
    }
    public OrderItem[] GetAllOrderProducts(int orderId)
    {
        OrderItem[] newArr = new OrderItem[4];//there is limit of up to 4 produts in order
        OrderItem p = new OrderItem();
        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            int j = 0;
            if (DataSource.orderItemArr[i].OrderId == orderId)//search for a certain order
            {
                p = DataSource.orderItemArr[i];
                newArr[j] = p;//add the orderItem that belongs to the order to the array
                j++;
            }
        }
        return newArr;
    }
    public OrderItem GetByProductIdAndOrderId(int orderId, int productId)
    {
        OrderItem p = new OrderItem();
        for (int i = 0; i < DataSource.arrOrderItemIndex; i++)
        {
            if (DataSource.orderItemArr[i].OrderId == orderId)//search for order
            {
                if (DataSource.orderItemArr[i].ProductId == productId)//search for product
                {
                    p = DataSource.orderItemArr[i];//return orderItem
                    return p;
                }
            }
        }
        throw new Exception("The item doesn't exist");
    }

}


