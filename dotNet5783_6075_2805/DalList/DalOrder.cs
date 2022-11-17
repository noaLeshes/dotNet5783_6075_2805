using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalOrder
{
    public int Add(Order order)
    {
        if (DataSource.arrOrderIndex == 100)
            throw new Exception("The array is full");//if the array is full throw
        order.ID = DataSource._nextOrderNumber;//updating the running number
        for (int i = 0; i<DataSource.arrOrderIndex; i++)
        {
            if (DataSource.orderArr[i].ID == order.ID)//if already exsist
                throw new Exception("The order is already existing");
        }
        DataSource.orderArr[DataSource.arrOrderIndex++] = order;
        return order.ID;
      
    }
    public Order GetById(int id)
    {
        Order d;
        for(int i=0; i<DataSource.arrOrderIndex; i++)
        {
            d = DataSource.orderArr[i];
            if (d.ID == id)//serch the array to find the wanted id
                return d;
        }
        throw new Exception("no order found");
    }
    public void Update(Order order)
    {
        for (int i = 0; i < DataSource.arrOrderIndex; i++)
        {
            if (DataSource.orderArr[i].ID == order.ID)
            {
                DataSource.orderArr[i] = order;//updating the wanted order
                return;
            }
            
        }
            throw new Exception("no order found");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.arrOrderIndex; i++)
        {
            if (DataSource.orderArr[i].ID == id)
            {
                DataSource.orderArr[i] = DataSource.orderArr[DataSource.arrOrderIndex];
                //taking the last order and replacing it with what we want to delete
                DataSource.arrOrderIndex--;//reducing the index to delete order
                return;
            }

        }
        throw new Exception("not found");//if not found
    }
    public Order[] GetAll()
    {
        Order[] newArr = new Order[DataSource.arrOrderIndex];
        for (int i = 0; i < DataSource.arrOrderIndex; i++)
        {
            newArr[i] = DataSource.orderArr[i];//copy all the orders to the new array
        }
        return newArr;
    }
}



