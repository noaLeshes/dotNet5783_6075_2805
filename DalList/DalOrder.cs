
using DalApi;
using DO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

internal class DalOrder : IOrder
{
    public int Add(Order order)
    {
        order.ID = DataSource._nextOrderNumber;//updating the running number
        bool isExist = DataSource.OrderList.Contains(order);
        if (isExist)
            throw new Exception("not found");
        DataSource.OrderList.Add(order);
        return order.ID;

    }
    public Order GetById(int id)
    {
        Order o = DataSource.OrderList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        return o;
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
        throw new Exception("no order found");
    }
    public void Delete(int id)
    {
        Order o = DataSource.OrderList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        DataSource.OrderList.Remove(o);
    }

    public IEnumerable<Order?> GetAll()
    {
        List<Order?> newList = new List<Order?>();
        foreach (Order o in DataSource.OrderList)
        {
            newList.Add(o);
        }
        return newList;
    }
}



