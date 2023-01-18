using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    readonly string s_orders = "orders";
    public int Add(Order item)
    {
        List<DO.Order?> o = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (o.FirstOrDefault(x => x?.ID == item.ID) != null)
        {
            throw new DalAlreadyExistsIdException(item.ID, "Order");
        }
        o.Add(item);
        XMLTools.SaveListToXMLSerializer(o, s_orders);
        return item.ID;
    }

    public void Delete(int id)
    {
        List<DO.Order?> o = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (o.RemoveAll(x => x?.ID == id) == 0)
        {
            throw new DalMissingIdException(id, "Order");
        };
        XMLTools.SaveListToXMLSerializer(o, s_orders);
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        List<DO.Order?> o = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (filter == null)
        {
            return o.Select(x => x).OrderBy(x => x?.ID);
        }
        else
        {
            return o.Where(filter).OrderBy(x => x?.ID);
        }
    }

    public Order GetByFilter(Func<Order?, bool>? filter = null)
    {
        List<DO.Order?> o = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return o.Find(x => filter!(x)) ?? throw new DalMissingIdException(-1, "Order");
    }

    public Order GetById(int id)
    {
        List<DO.Order?> o = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return o.FirstOrDefault(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Order");
    }

    public void Update(Order item)
    {
        Delete(item.ID);
        Add(item);
    }
}
