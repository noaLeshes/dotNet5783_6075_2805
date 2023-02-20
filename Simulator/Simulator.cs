using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public static class Simulator
    {
        private static readonly Random rand = new();
        public static bool activate = true;
        public delegate void Report1(Order o, OrderStatus before,OrderStatus after, int delay);
        public static Report1? report1;
        public delegate void Report2(string msg);
        public static Report2? report2;
        public delegate void Report3(string msg);
        public static Report3? report3;
        public static void Activate()
        {
            activate = true;
            BlApi.IBl? bl = BlApi.Factory.Get();
            new Thread(() =>
            {
                while (activate)
                {
                    int? orderId = bl.Order.getOldest();
                    if (orderId != null)
                    {
                        Order o = bl.Order.GetOrderDitailes((int)orderId);
                        int delay = rand.Next(3, 11);
                        DateTime t = DateTime.Now + new TimeSpan(delay * 1000);
                        OrderStatus final;
                        if (o.Status == OrderStatus.Ordered)
                        {
                            final = OrderStatus.Shipped;
                            report1(o, o.Status, final, delay);
                            bl.Order.UpdateShipping((int)orderId);
                            Thread.Sleep(delay * 1000);
                        }
                        else if(o.Status == OrderStatus.Shipped)
                        {
                            final = OrderStatus.Delivered;
                            report1(o, o.Status, final, delay);
                            bl.Order.UpdateIfProvided((int)orderId);
                            Thread.Sleep(delay * 1000);
                        }
                        if (activate)
                         report2("finished updating order number: ");
                    }
                    else
                        activate = false;
                    Thread.Sleep(1000);
                }
                report3("finshed Simulation");
            }).Start();
        }
        public static void Register1(Report1 r1)
        {
            report1 += r1;
        }
        public static void Register2(Report2 r2)
        {
            report2 += r2;
        }
        public static void Register3(Report3 r3)
        {
            report3 += r3;

        }
        public static void UnRegister1(Report1 r1)
        {
            report1 -= r1;

        }
        public static void UnRegister2(Report2 r2)
        {
            report2 -= r2;

        }
        public static void UnRegister3(Report3 r3)
        {
            report3 -= r3;
        }

    }
}
