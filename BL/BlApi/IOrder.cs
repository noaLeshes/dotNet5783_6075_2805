﻿using BO;
namespace BlApi;

public interface IOrder
{
    IEnumerable<OrderForList?> GetOrders();// get list of orders
    Order GetOrderDitailes(int id);// get by id
    Order UpdateShipping(int id);// update shipping status
    Order UpdateIfProvided(int id);// update delivering status
    OrderTracking GetOrderTracking(int id);// get the order tracking
}
