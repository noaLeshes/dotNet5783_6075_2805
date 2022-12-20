﻿using DO;
namespace Dal;
internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();
    internal static List<Product?> ProductList { get; } = new List<Product?>();
    internal static List<Order?> OrderList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemList { get; } = new List<OrderItem?>();
    private static void s_Initialize()
    {
        createAndInitProducts();
        createAndInitOrders();
        createAndInitOrderItems();
    }
    /// <summary>
    /// creating the runing numbers for the add func
    /// </summary>
    internal const int startOrderNumber = 100000;
    private static int nextOrderNumber = startOrderNumber;
    internal static int _nextOrderNumber { get => nextOrderNumber++; }//order
    internal const int startOrderItemNumber = 100000;
    private static int nextOrderItemNumber = startOrderItemNumber;
    internal static int _nextOrderItemNumber { get => nextOrderItemNumber++; }//orderItem

    //matrix of products
    private static string[,] productNames = new string[5, 3] { { "EyeLiner", "Mascara", "Eyeshadow-Palette" }, //eyes
                                                  { "Foundation","Concealer", "Blush" },// face
                                                  { "Eyeshadow-Brush", "Blender-Brush", "Blush-Brush"},// brushes
                                                  {"LipStick","Gloss", "Lip-Liner" },// lips
                                                  {"Face-Mask","Moisturizer", "Makeup-Wipes" }/*beauty*/ };
    private static void createAndInitProducts()//product constructor
    {
        for (int i = 0; i < 10; i++)//creating 10 products
        {
            int c = s_rand.Next(5);
            int n = s_rand.Next(3);
            ProductList.Add(new Product//add the product to the array
            {
                ID = i + 100000,
                Name = productNames[c, n],//get the product's name from the matrix
                Price = s_rand.Next(50, 250),
                Category = (Category)c,
                InStock = s_rand.Next(50)
            });
        }
    }

    private static void createAndInitOrders()//order constructor
    {
        /// <summary>
        /// arrays of names and adresses
        /// </summary>
        string[] firstNames = { "Moshe", "Yosi", "Avi", "Adi", "Rachel" };
        string[] lastNames = { "Cohen", "Levi", "Miller", "David", "Hadad" };
        string[] arrayAdresses = {"Hadekel 1", "Hadekel 2", "Hadekel 3", "Hadekel 4", "Hadekel 5",
                                  "Pinkas 1", "Pinkas 2", "Pinkas 3","Pinkas 4", "Pinkas 5",
                                  "Herzl 1", "Herzl 2", "Herzl 3", "Herzl 4", "Herzl 5",
                                  "Bagno 1", "Bagno 2", "Bagno 3", "Bagno 4", "Bagno 5", };
        for (int i = 0; i < 20; i++)//creating 20 orders
        {
            int days = s_rand.Next(200);
            DateTime orderDate = DateTime.Now.AddDays(-days); // order date is berfore current date
            DateTime? deliveryDate = null;
            DateTime? shipDate = null;
            TimeSpan timeSpan;
            if (i < 0.8 * 20) // 5% doesnt get delivery and ship date
            {
                days = s_rand.Next(10, 20);
                timeSpan = new TimeSpan(days, 0, 0, 0);
                shipDate = orderDate + timeSpan;
            }
            if (i < 0.8 * 0.6 * 20)
            {
                days = s_rand.Next(1, 10);
                timeSpan = new TimeSpan(days, 0, 0, 0);
                if (shipDate != null)
                    deliveryDate = shipDate + timeSpan;
                else
                    deliveryDate = DateTime.MinValue + timeSpan;
            }
            string firstName = firstNames[s_rand.Next(5)];
            string lastName = lastNames[s_rand.Next(5)];
            OrderList.Add(new Order                        //add the order to the array
            {
                ID = _nextOrderNumber,
                OrderDate = orderDate,
                ShipDate = shipDate,
                DeliveryDate = deliveryDate,
                CustomerName = firstName + " " + lastName,
                CustomerAddress = arrayAdresses[s_rand.Next(20)],
                CustomerEmail = firstName + lastName + "@gmail.com"
            });


        }
    }
    private static int[] amounts = new int[5] { 2, 25, 15, 3, 5 };
    private static void createAndInitOrderItems()//orderItem constructor
    {
        for (int i = 0; i < 20; i++)//creating 40 orderitems
        {
            int amountOfOrderItems = s_rand.Next(1, 5);//between 1 to 4 items per order
            for (int j = 0; j < amountOfOrderItems; j++)
            {
                int num = s_rand.Next(9);
                Product? p = ProductList[num];
                while (p?.InStock == 0)// if not in stock find another product
                {
                     num = s_rand.Next(9);
                     p = ProductList[num];
                }
                int amount = s_rand.Next(10);
                OrderItemList.Add(new OrderItem//add the order-item to the array
                {
                    Amount = amount,
                    Price = p?.Price??0,//caculate the price
                    ID = _nextOrderItemNumber,
                    ProductId = p?.ID??0,
                    OrderId = OrderList[i]?.ID
                }) ;
            }
        }
    }
}