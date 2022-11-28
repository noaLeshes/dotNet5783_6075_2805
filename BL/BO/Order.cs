﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; } 
    public string CostumerName { get; set; }
    public string CostumerAdress { get; set; }  
    public DateTime orderDate { get; set; } 
    public OrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }   
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public OrderItem Items { get; set; }    
    public double TotalPrice { get; set; }   

}
