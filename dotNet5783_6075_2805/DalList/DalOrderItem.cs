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
        throw new Exception("no order found");
    }
    public void Update(OrderItem oi)
    {

        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == oi.ID)
                DataSource.OrderItemArr[i] = oi;
        }

    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
        {
            if (DataSource.OrderItemArr[i].ID == id)
            {
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[DataSource.Config.arrOrderItemIndex];
                DataSource.OrderArr[DataSource.Config.arrOrderIndex] = 0;
                DataSource.Config.arrOrderItemIndex--;
            }
        }
    }
    public IEnumerable<Order?> GetAll()
    {

    }
}
public static Product[] ProductsInOrder(int orderId)
{
    Product[] newArr = new Product[DataSource.Config.arrOrderItemIndex];
    Product p;
    for (int i = 0; i < DataSource.Config.arrOrderItemIndex; i++)
    {
        if (DataSource.OrderItemArr[i].OrderId == orderId)
        {
           // p = DalProduct.GetById(DataSource.OrderItemArr[i].ProductId);//??????????????????????????????????????
            newArr[i] = p;  
        }   
    }
    return newArr[];
}
