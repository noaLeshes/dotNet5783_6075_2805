using DO;
using System.Diagnostics;
using System.Xml.Linq;
using static Dal.DataSource;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();
    internal static Product[] ProductArr { get; } = new Product[50];
    internal static Order[] OrderArr { get; } = new Order[100];
    internal static OrderItem[] OrderItemArr { get; } = new OrderItem[200];
    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }
    internal static class Config
    {
        static internal int arrProductIndex = 0;
        static internal int arrOrderIndex = 0;  
        internal static int arrOrderItemIndex = 0;  
        internal const int s_startOrderNumber = 1000;
         private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }//order
        internal const int s_startOrderItemNumber = 1000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }//orderItem
    }

    private static string[,] productNames = new string[5, 3] { { "EyeLiner", "Mascara", "Eyeshadow-Palette" }, //eyes
                                                  { "foundation","Concealer", "Blush" },// face
                                                  { "Eyeshadow-Brush", "Blender-Brush", "Blush-Brush"},// brushes
                                                  {"LipStick","Gloss", "Lip-Liner" },// lips
                                                  {"Face-Mask","Moisturizer", "Makeup-Wipes" }/*beauty*/ };
    private static void createAndInitProducts()
    {
        for (int i = 0; i < 10; i++)
        {
            int c = s_rand.Next(4);
            int n = s_rand.Next(2);
            ProductArr[i] = new Product
            {
                ID = i+100000,
                Name = productNames[c,n],
                Price = s_rand.Next(200),
                Category = (Category)c,
                InStock = s_rand.Next(50)//ask orit????/????//////?????////
            };
        }
    }
    private static void createAndInitOrders()
    {
        string[] firstNames = { "Moshe", "Yosi", "Avi", "Adi" , "Rachel" };
        string[] lastNames = { "Cohen", "Levi", "Miller", "David", "Hadad" };
        string[] arrayAdresses = {"Hadekel 1", "Hadekel 2", "Hadekel 3", "Hadekel 4", "Hadekel 5",
                                  "Pinkas 1", "Pinkas 2", "Pinkas 3","Pinkas 4", "Pinkas 5",
                                  "Herzl 1", "Herzl 2", "Herzl 3", "Herzl 4", "Herzl 5",
                                  "Bagno 1", "Bagno 2", "Bagno 3", "Bagno 4", "Bagno 5", };
        for (int i = 0; i < 20; i++)
        {
            
            int days = s_rand.Next(1000);
            DateTime orderDate = DateTime.Now.AddDays(-days);
            DateTime? deliveryDate;
            DateTime? shippingDate;
            if(i%5==0)
            {
                deliveryDate = null;
                shippingDate= null; 
            }
            else
            {
                days = s_rand.Next(1, 3);
                TimeSpan timeS1 = new TimeSpan(days, 0, 0, 0);
                deliveryDate = orderDate + timeS1;
                if((i+2)%3==0)
                {
                    shippingDate = null;    
                }
                else
                {
                    days = s_rand.Next(3, 7);
                    TimeSpan timeS2 = new TimeSpan(days, 0, 0, 0);
                    shippingDate=orderDate + timeS2;
                }
            }

            string firstName = firstNames[s_rand.Next(4)];
            string lastName = lastNames[s_rand.Next(4)];
            OrderArr[i] = new Order
            {
                ID = Config.NextOrderNumber,
                OrderDate = orderDate,
                ShipDate = shippingDate,
                DeliveryDate = deliveryDate,
                CustomerName = firstName + " " + lastName,
                CustomerAddress = arrayAdresses[s_rand.Next(9)] ,
                CustomerEmail = firstName + lastName + "@gmail.com"

            };
            Config.arrProductIndex++;
        }
    }


    private static int[] amounts = new int[5] { 5, 5, 5, 5, 5 };
    private static void createAndInitOrderItems()
    {
        int numberOfOrder = 1;
        for (int i = 0; i < 40; i++)
        {
            int amountOfOrderItems = s_rand.Next(1, 4);
            for (int j = 0; j < amountOfOrderItems; j++)
            {
                Product p = ProductArr[s_rand.Next(amountOfOrderItems)];
                int amount = s_rand.Next(1, amounts[(int)p.Category]);//??????
                OrderItemArr[i] = new OrderItem
                {
                    Amount = amount ,
                    Price = p.Price*amount,
                    ID = Config.NextOrderItemNumber,
                    ProductId = p.ID,
                    OrderId = OrderArr[i].ID,
                };
                //numOfOrderItems++; ?????????????
            }
            numberOfOrder++;
        }
    }




}
