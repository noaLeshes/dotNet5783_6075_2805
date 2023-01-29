using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class Simulator
{
    private static readonly Random rand = new ();
    public static void Activate()
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        new Thread(() =>
        {
            bool activate = true;
            while (activate)
            {
                int? orderId = bl.Order.getOldest();
                if (orderId != null)
                {
                    Order o = bl.Order.GetOrderDitailes((int)orderId);
                    int delay = rand.Next(3, 11);
                    DateTime t = DateTime.Now + new TimeSpan(delay * 1000);
                    //report1(o, o.Status, DateTime.Now, o.Status + 1, t);
                    Thread.Sleep(delay * 1000);
                    //report2("finished updating order");
                    if (o.Status == OrderStatus.Ordered)
                        bl.Order.UpdateShipping((int)orderId);
                    else if (o.Status == OrderStatus.Shipped)
                        bl.Order.UpdateIfProvided((int)orderId);
                }
                Thread.Sleep(1000);
            }
            //report3("finshed Simulation");
        }).Start();
    }
}
