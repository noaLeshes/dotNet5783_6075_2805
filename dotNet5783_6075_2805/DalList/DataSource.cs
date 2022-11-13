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
        int c = s_rand.Next(4);
        int n = s_rand.Next(2);
        for (int i = 0; i < 10; i++)
        {
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
        string[] arrayAdresses = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        for (int i = 0; i < 20; i++)
        {
            string firstName = firstNames[s_rand.Next(4)];
            string lastName = lastNames[s_rand.Next(4)];

            OrderArr[i] = new Order
            {
                ID = Config.NextOrderNumber,
                OrderDate = DateTime.MinValue,
                // ShipDate = TimeSpan
                //DeliveryDate
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
