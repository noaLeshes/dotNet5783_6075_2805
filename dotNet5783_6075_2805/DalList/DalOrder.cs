using DO;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

public class DalOrder
{
    public int Add(Order order)
    {
        if (DataSource.Config.arrOrderIndex == 100)
            throw new Exception("The array is full");
        for(int i = 0; i<DataSource.Config.arrOrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == order.ID)    
                throw new Exception("The order is already existing");
        }
        DataSource.OrderArr[DataSource.Config.arrOrderIndex] = order;
        return order.ID;
      
    }
    public Order GetById(int id)
    {
        Order d;
        for(int i=0; i<DataSource.Config.arrOrderIndex; i++)
        {
            d = DataSource.OrderArr[i];
            if (d.ID == id)
                return d;
        }
        throw new Exception("no order found");
    }
    public void Update(Order order)
    {
        for (int i = 0; i < DataSource.Config.arrOrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == order.ID)
                DataSource.OrderArr[i] = order;
            else
                throw new Exception("no order found");
        }

    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.arrOrderIndex; i++)
        {
            if (DataSource.OrderArr[i].ID == id)
            {
                DataSource.OrderArr[i] = DataSource.OrderArr[DataSource.Config.arrOrderIndex];
                //DataSource.OrderArr[DataSource.Config.arrOrderIndex] = 0;
                DataSource.Config.arrOrderIndex--;
            }

        }
    }
    public IEnumerable<Order?> GetAll()
    {

    }
}
