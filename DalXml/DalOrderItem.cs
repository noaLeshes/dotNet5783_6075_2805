using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    readonly string s_orderItems = "orderItems";
    public int Add(OrderItem item)
    {
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (oi.FirstOrDefault(x => x?.ID == item.ID) != null)
        {
            throw new DalAlreadyExistsIdException(item.ID, "OrderItem");
        }
        item.ID = Config.GetNextOrderItemId();
        oi.Add(item);
        Config.SetNextOrderItemId(item.ID + 1);
        XMLTools.SaveListToXMLSerializer(oi, s_orderItems);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (oi.RemoveAll(x => x?.ID == id) == 0)
        {
            throw new DalMissingIdException(id, "OrderItem");
        };
        XMLTools.SaveListToXMLSerializer(oi, s_orderItems);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (filter == null)
        {
            return oi.Select(x => x).OrderBy(x => x?.ID);
        }
        else
        {
            return oi.Where(filter).OrderBy(x => x?.ID);
        }
    }

    public OrderItem GetByFilter(Func<OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        return oi.Find(x => filter!(x)) ?? throw new DalMissingIdException(-1, "OrderItem");
    }

    public OrderItem GetById(int id)
    {
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        return oi.FirstOrDefault(x => x?.ID == id) ?? throw new DalMissingIdException(id, "OrderItem");
    }

    public void Update(OrderItem item)
    {
        Delete(item.ID);
        List<DO.OrderItem?> oi = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (oi.FirstOrDefault(x => x?.ID == item.ID) != null)
        {
            throw new DalAlreadyExistsIdException(item.ID, "OrderItem");
        }
        oi.Add(item);
        XMLTools.SaveListToXMLSerializer(oi, s_orderItems);
    }
}
