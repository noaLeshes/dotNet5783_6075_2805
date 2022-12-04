using BlApi;
using BO;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.DalList();
    BO.Order IOrder.GetOrderDitailes(int id)
    {
        if(id< 0)   
            throw new NotImplementedException();
         
    }

    IEnumerable<OrderForList?> IOrder.GetOrders()
    {
        throw new NotImplementedException();
    }

    OrderTracking IOrder.GetOrderTracking(int id)
    {
        throw new NotImplementedException();
    }

    BO.Order IOrder.UpdateIfProvided(int id)
    {
        throw new NotImplementedException();
    }

    BO.Order IOrder.UpdateShipping(int id)
    {
        throw new NotImplementedException();
    }
}
