using System;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList?> GetOrders();
    Order GetOrderDitailes(int id);
    Order UpdateShipping(int id);
    Order UpdateIfProvided(int id);
    OrderTracking GetOrderTracking(int id);
}
