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
        try
        {
            IEnumerable<DO.OrderItem?> orderItems = dal.OrderItem.GetAllOrderProducts(o.ID);
            IEnumerable<BO.OrderItem?> b_orderItems = new List<BO.OrderItem?>(); //create item for BO.Order
            DO.Product p;//product to find name
            //Create Items for order:
                b_orderItems = from DO.OrderItem x in orderItems
                                   select new BO.OrderItem()
                                   {
                                       Id = x.ID ,
                                       Name = dal.Product.GetById(x.ProductId ).Name ?? " ",
                                       ProductId = x.ProductId ,
                                       Price = x.Price ,
                                       Amount = x.Amount ,
                                       TotalPrice = x.Price * x.Amount 
                                   };
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
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }
    private OrderStatus findStatus(DO.Order o)
    {
        OrderStatus stat = OrderStatus.Initiated;
        if(o.DeliveryDate != null &&o.OrderDate == null || (o.ShipDate != null && o.OrderDate == null))
            throw new BlIncorrectDateException("OrderDate not updated ");
        if (o.DeliveryDate != null && o.ShipDate == null)
            throw new BlIncorrectDateException("shipping not updated ");
        if (o.OrderDate != null)
            stat = OrderStatus.Ordered;
        if (o.ShipDate != null)
            stat = OrderStatus.Shipped;
        if (o.DeliveryDate != null)
            stat = OrderStatus.Delivered;
        return stat;
    }
    public BO.Order GetOrderDitailes(int id)
    {
        try
        {
            if (id < 0)
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Order o = dal.Order.GetById(id);
            return createOrder(o);
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }

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
                CustomerName = o.CustomerName,
                AmountOfItems = orderitems.Count(),
                TotalPrice = orderitems.Sum(x => x?.Price ?? 0 ),
                Status = findStatus(o)
            }) ;
        }
        return orderforlists;
    }

    public OrderTracking GetOrderTracking(int id)
    {
        try
        {
            DO.Order o = dal.Order.GetById(id);
            List<Tuple<DateTime?, string>> list = new List<Tuple<DateTime?, string>>();
            if (o.OrderDate != null )
                list.Add(Tuple.Create(o.OrderDate, "your order has been accepted"));
            if (o.ShipDate != null)
                list.Add(Tuple.Create(o.OrderDate, "your order has been shipped "));
            if (o.DeliveryDate != null)
                list.Add(Tuple.Create(o.OrderDate, "your order has been deliverd "));

            return new BO.OrderTracking()
            {
                ID = id,
                Status = findStatus(o),
                Tracking = list
            };
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }

    public BO.Order UpdateIfProvided(int id)
    {
        try
        {
            BO.Order t = new();
            DO.Order o = dal.Order.GetById(id);
            if (o.DeliveryDate != null)
                throw new BlIncorrectDateException("delevery already updated ");
            if ( o.ShipDate == null)
                throw new BlIncorrectDateException("shipping not updated");
            if (o.DeliveryDate == null && o.ShipDate != null)
            {
                o.DeliveryDate = DateTime.Now;
                dal.Order.Update(o);
                t = createOrder(o);
            }

            return t;
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }

    public BO.Order UpdateShipping(int id)
    {
        try
        {
            DO.Order o = dal.Order.GetById(id);
            if (o.ShipDate == null && o.OrderDate != null)
            {
                o.ShipDate = DateTime.Now;
                dal.Order.Update(o);
            }
            else if (o.OrderDate == null)
                throw new BlIncorrectDateException("orderdate not updated");
            else
                throw new BlIncorrectDateException("shipping already updated ");
            return createOrder(o);
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }
}



