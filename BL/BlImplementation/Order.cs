using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    private BO.Order createOrder(DO.Order? o)
    {
        try
        {
            IEnumerable<DO.OrderItem?>? orderItems = dal?.OrderItem.GetAll(x=> x?.OrderId == o?.ID);//get all the orders
            IEnumerable<BO.OrderItem?> b_orderItems = new List<BO.OrderItem?>(); //create list of BO.OrderItems
            //Create Items for order:
                b_orderItems = from DO.OrderItem x in orderItems!  //add every OrderItem ass BO to list of items
                               select new BO.OrderItem() 
                                   {
                                       Id = x.ID ,
                                       Name = dal?.Product.GetById(x.ProductId ).Name ?? " ",
                                       ProductId = x.ProductId ,
                                       Price = x.Price ,
                                       Amount = x.Amount ,
                                       TotalPrice = x.Price * x.Amount 
                                   };
            return new BO.Order() //creat new BO order
            {
                ID = o?.ID ?? 0,
                CostumerName = o?.CustomerName,
                CostumerAdress = o?.CustomerAddress,
                orderDate = o?.OrderDate,
                ShipDate = o?.ShipDate,
                DeliveryDate = o?.DeliveryDate,
                PaymentDate = DateTime.Now,
                Status = findStatus(o),
                Items = b_orderItems,
                TotalPrice = b_orderItems.Sum(x => x!.Amount * x.Price)
            };
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }
    private OrderStatus findStatus(DO.Order? o)
    {
        OrderStatus stat = OrderStatus.Initiated;
        if (o?.DeliveryDate != null && o?.OrderDate == null || (o?.ShipDate != null && o?.OrderDate == null))
            throw new BlIncorrectDateException("OrderDate not updated ");//if DeliveryDate\ShipDate is updated and OrderDate is not
        if (o?.DeliveryDate != null && o?.ShipDate == null)
            throw new BlIncorrectDateException("shipping not updated ");//if DeliveryDate is updated and ShipDate is not
        if (o?.OrderDate != null)
            stat = OrderStatus.Ordered;
        if (o?.ShipDate != null)
            stat = OrderStatus.Shipped;
        if (o?.DeliveryDate != null)
            stat = OrderStatus.Delivered;
        return stat;
    }
    public BO.Order GetOrderDitailes(int id)
    {
        try
        {
            if (id < 0)//not valid id
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            DO.Order? o = dal?.Order.GetById(id);// find the wanted order
            return createOrder(o);//turn DO.order into BO.order
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }

    }

    public IEnumerable<OrderForList?> GetOrders()
    {
        List<BO.OrderForList?> orderforlists = new();
        var orders = dal?.Order.GetAll();//get all the DO.orders
        orderforlists.AddRange(from DO.Order o in orders!
                               let orderitems = dal?.OrderItem.GetAll(x=> x?.OrderId == o.ID)
                               select new BO.OrderForList()// adds and create OrderForList to orderforlists 
                               {
                              ID = o.ID,
                              CustomerName = o.CustomerName,
                              AmountOfItems = dal?.OrderItem.GetAll(x => x?.OrderId == o.ID).Sum(x => x?.Amount) ?? 0,//count the number of orderitems in order
                              TotalPrice = orderitems.Sum(x => x?.Price ?? 0),// summarize all the products price
                              Status = findStatus(o) // find the current status
                          });
     
        return orderforlists;
    }

    public OrderTracking GetOrderTracking(int id)
    {
        try
        {
            DO.Order? o = dal?.Order.GetById(id);
            List<Tuple<DateTime?, string>> list = new List<Tuple<DateTime?, string>>();// create new list<Tuple>
            if (o?.OrderDate != null)// check if OrderDate updated
                list.Add(Tuple.Create(o?.OrderDate, "your order has been accepted")); //add to Tracking
            if (o?.ShipDate != null)// check if ShipDate updated
                list.Add(Tuple.Create(o?.ShipDate, "your order has been shipped ")); //add to Tracking
            if (o?.DeliveryDate != null)// check if DeliveryDate updated
                list.Add(Tuple.Create(o?.DeliveryDate, "your order has been deliverd ")); //add to Tracking

            return new BO.OrderTracking()
            {
                ID = id,
                Status = findStatus(o),// get status
                Tracking = list
            };
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }

    public BO.Order UpdateIfProvided(int id, DateTime? d)
    {
        try
        {
            BO.Order t = new();
            DO.Order o = dal!.Order.GetById(id);//find the wanted order
            if (o.DeliveryDate != null)  
                throw new BlIncorrectDateException("delevery already updated ");
            if ( o.ShipDate == null)
                throw new BlIncorrectDateException("shipping not updated");
            if (o.DeliveryDate == null && o.ShipDate != null)// DeliveryDate not updated and allready Shipped
            {
                if(d != null)
                    o.DeliveryDate = d;
                else
                    o.DeliveryDate = DateTime.Now;// updating DeliveryDate

                dal.Order.Update(o); // updating Order
                t = createOrder(o); // turn DO.order into BO.order
            }

            return t;
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }

    public BO.Order UpdateShipping(int id, DateTime? d = null)
    {
        try
        {
            DO.Order o = dal!.Order.GetById(id);
            if (o.ShipDate == null && o.OrderDate != null)
            {
                if (d != null)
                    o.ShipDate = d;
                else
                    o.ShipDate = DateTime.Now;
                dal.Order.Update(o); // updating Order
            }
            else if (o.OrderDate == null) 
                throw new BlIncorrectDateException("orderdate not updated");
            else 
                throw new BlIncorrectDateException("shipping already updated ");
            return createOrder(o);//turn DO.order into BO.order
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Order doesn't exist", exception);
        }
    }
}



