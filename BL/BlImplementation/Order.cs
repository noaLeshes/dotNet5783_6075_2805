using BlApi;
using BO;
using System.Collections;
using System.Collections.Generic;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    private BO.Order createOrder(DO.Order o)
    {
        var orderItems = dal.OrderItem.GetAllOrderProducts(o.ID);
        IEnumerable<BO.OrderItem?> b_orderItems = new List<BO.OrderItem?>(); //create item for BO.Order
        DO.Product p;//product to find name

        //Create Items for order:
        foreach (DO.OrderItem? item in orderItems)
        {
            p = dal.Product.GetById(item?.ID ?? 0);
            b_orderItems.ToList().Add(new BO.OrderItem()
            {
                Id = item?.ID ?? 0,
                Name = dal.Product.GetById(item?.ID ?? 0).Name ?? " ",
                ProductId = item?.ProductId ?? 0,
                Price = item?.Price ?? 0,
                Amount = item?.Amount ?? 0,
                TotalPrice = item?.Price * item?.Amount ?? 0
            });
        }

        return new BO.Order()
        {
            ID = o.ID,
            CostumerName = o.CustomerName,
            CostumerAdress = o.CustomerAddress,
            orderDate = o.OrderDate,
            ShipDate = o.ShipDate,
            DeliveryDate = o.DeliveryDate,
            PaymentDate = DateTime.Now,
            Status = findStatus(o),
            Items = b_orderItems,
            TotalPrice = b_orderItems.Sum(x => x.Amount * x.Price),
        };
    }
    private OrderStatus findStatus(DO.Order o)
    {
        OrderStatus stat = OrderStatus.Initiated;
        if (o.OrderDate < DateTime.Now)
            stat = OrderStatus.Ordered;
        if (o.ShipDate < DateTime.Now)
            stat = OrderStatus.Shipped;
        if (o.DeliveryDate < DateTime.Now)
            stat = OrderStatus.Delivered;
        return stat;
    }
    public BO.Order GetOrderDitailes(int id)
    {
        if(id< 0)   
            throw new NotImplementedException();
        DO.Order o = dal.Order.GetById(id);
        return createOrder(o);
         
    }

    public IEnumerable<OrderForList?> GetOrders()
    {
        List<BO.OrderForList?> orderforlists = new();
       var orders = dal.Order.GetAll();
        foreach (DO.Order o in orders)
        {
            IEnumerable<DO.OrderItem?> orderitems = dal.OrderItem.GetAllOrderProducts(o.ID);
            orderforlists.Add(new BO.OrderForList()
            {
                ID = o.ID,
                AmountOfItems = orderitems.Count(),
                TotalPrice = orderitems.Sum(x => x?.Price ?? 0 ),
                Status = findStatus(o)
            }) ;
        }
        return orderforlists;
    }

    public OrderTracking GetOrderTracking(int id)
    {
        DO.Order o = dal.Order.GetById(id);
        List<Tuple<DateTime?, string>> list = new List<Tuple<DateTime?, string>>();
        if (o.OrderDate != null)
            list.Add(Tuple.Create(o.OrderDate, "your order has been accepted"));
        if(o.ShipDate != null)
            list.Add(Tuple.Create(o.OrderDate, "your order has been shipped "));
        if (o.DeliveryDate != null)
            list.Add(Tuple.Create(o.OrderDate, "your order has been deliverd "));
        return new BO.OrderTracking()
        {
            ID=id,
            Status = findStatus(o),
            Tracking = list
        };
    }

    public BO.Order UpdateIfProvided(int id)
    {
        DO.Order o = dal.Order.GetById(id);
        if ( o.DeliveryDate > DateTime.Now)
        {
            o.DeliveryDate = DateTime.Now.AddDays(5);
            dal.Order.Update(o);
        }
        return createOrder(o);
    }

    public BO.Order UpdateShipping(int id)
    {
        DO.Order o = dal.Order.GetById(id);
        if (o.ShipDate > DateTime.Now )
        {
            o.ShipDate = DateTime.Now.AddDays(5);
            dal.Order.Update(o);
        }
        return createOrder(o);
    }
}



