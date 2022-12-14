using DO;
using DalApi;
namespace Dal;


public enum MainChoice { End = 0, Product, Order, OrderItem }
public enum SecondaryChoice { Add = 1, Delete, Update, GetById, GetAll, GetAllOrderProducts, GetByProductIdAndOrderId }
internal class Program
{
    //private static DalProduct dalProduct = new DalProduct();
    //private static DalOrder dalOrder = new DalOrder();
    //private static DalOrderItem dalOrderItem = new DalOrderItem();
    private static IDal dal = new DalList();
    private static void productFunc()
    {
        Console.WriteLine(@"please enter your choice:
                            1 - adding a product
                            2 - deleting a product
                            3 - updating a product
                            4 - getting a product by its id
                            5 - getting all the products");
        try
        {
            SecondaryChoice productChoice = SecondaryChoice.Add;//initalize the choise
            int myId, myInStock;
            string? myName;
            double myPrice;
            Category myCategory;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out productChoice))
            {
                switch (productChoice)
                {
                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the product's id: ");//getting details from user
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//if not valid
                        Console.WriteLine("Enter the product's name: ");
                        myName = Console.ReadLine();
                        
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");//if not valid
                        Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2 - BRUSHES
                                           3 - LIPS
                                           4 - BEAUTY");
                        if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 4 || (int)myCategory < 0) throw new Exception("incorrect category");
                        Console.WriteLine("Enter the amount of products in stock: ");
                        if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new Exception("incorrect amount");//if not valid
                        Product p = new Product //initalize the new product
                        {
                            ID = myId,
                            Name = myName,
                            Price = myPrice,
                            Category = myCategory,
                            InStock = myInStock,
                        };
                        dal.Product.Add(p);
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the product's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//if not valid
                        dal.Product.Delete(myId);
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the product's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//if not valid
                        Console.WriteLine(dal.Product.GetById(myId)); //find the product we want to update
                        Console.WriteLine("Enter product's details to update: ");//recive the changes we want to update
                        Console.WriteLine("Enter the product's name: ");
                        myName = Console.ReadLine();
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2 - BRUSHES
                                           3 - LIPS
                                           4 - BEAUTY");
                        if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 4 || (int)myCategory < 0) throw new Exception("incorrect category");
                        Console.WriteLine("Enter the amount of products in stock: ");
                        if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new Exception("incorrect amount");
                        Product p1 = new Product //updating the changes the user gave
                        {
                            ID = myId,
                            Name = myName,
                            Price = myPrice,
                            Category = myCategory,
                            InStock = myInStock,
                        };
                        dal.Product.Update(p1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                        Product p2 = dal.Product.GetById(myId);// find the wanted product
                        Console.WriteLine(p2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the products: ");
                        IEnumerable<Product?> newProductArr = dal.Product.GetAll();
                        foreach (var p3 in newProductArr)
                        {
                            Console.WriteLine(p3);//print all the producs
                        }
                        break;
                    default: throw new Exception("Error");
                }
            }
            else
            {
                throw new Exception("incorrect choice");// for unknown choice
            }
        }
        catch (Exception newException)
        {
            Console.WriteLine(newException.ToString());
        }
    }
    private static void orderFunc()
    {
        Console.WriteLine(@"please enter your choice:
                            1 - adding an order
                            2 - deleting an order
                            3 - updating an order
                            4 - getting an order by its id
                            5 - getting all the orders");
        try
        {
            SecondaryChoice orderChoice = SecondaryChoice.Add;//initalize the choise
            int myId;
            string? myCustomerName, myCustomerEmail, myCustomerAddress;
            DateTime myOrderDate;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out orderChoice))
            {
                switch (orderChoice)
                {
                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the costumer's name: ");//getting details from user
                        myCustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's email: ");
                        myCustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's address: ");
                        myCustomerAddress = Console.ReadLine();
                        myOrderDate = DateTime.Now;
                        Order o = new Order //initalize the new order
                        {
                            //ID = myId,
                            CustomerName = myCustomerName,
                            CustomerEmail = myCustomerEmail,
                            CustomerAddress = myCustomerAddress,
                            OrderDate = myOrderDate,
                            ShipDate = /*null*/myOrderDate.AddHours(10),
                            DeliveryDate = /*null*/myOrderDate.AddDays(2),
                        };
                        dal.Order.Add(o);
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the order's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                        dal.Order.Delete(myId);//deleting
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the order's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                        Console.WriteLine(dal.Order.GetById(myId));//find the order we want to update
                        Console.WriteLine("Enter product's details to update: ");//geting changes from user
                        Console.WriteLine("Enter the costumer's name: ");
                        myCustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's email: ");
                        myCustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's address: ");
                        myCustomerAddress = Console.ReadLine();
                        Console.WriteLine("Enter the order's date: ");
                        if (DateTime.TryParse(Console.ReadLine(), out myOrderDate) == false) throw new Exception("incorrect id");//throw if not valid

                        Order o1 = new Order // updating the order with the changes the user gave
                        {
                            ID = dal.Order.GetById(myId).ID,
                            CustomerName = myCustomerName,
                            CustomerEmail = myCustomerEmail,
                            CustomerAddress = myCustomerAddress,
                            OrderDate = myOrderDate,
                            ShipDate = myOrderDate.AddHours(10),
                            DeliveryDate = myOrderDate.AddDays(2),
                        };
                        dal.Order.Update(o1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//invalid input
                        Order o2 = dal.Order.GetById(myId);
                        Console.WriteLine(o2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the orders: ");
                        IEnumerable<Order?> newOrderArr = dal.Order.GetAll();
                        foreach (var o3 in newOrderArr)
                        {
                            Console.WriteLine(o3); // print all the orders in the array
                        }
                        break;
                    default: throw new Exception("Error");
                }
            }
            else
            {
                throw new Exception("incorrect choice");// unknown choice
            }
        }
        catch (Exception newException)
        {
            Console.WriteLine(newException.ToString());
        }
    }
    private static void orderItemFunc()
    {
        Console.WriteLine(@"please enter your choice:
                            1 - adding a product to an order
                            2 - deleting a product from an order
                            3 - updating a product in an order
                            4 - getting a product in an order by its id
                            5 - getting all the products in an order
                            6- getting all the products of a specific order
                            7- getting a product by its product id and order id");
        try
        {
            SecondaryChoice orderItemChoice = SecondaryChoice.Add;//initalize the choise
            int myId, myOrderId, myProductId, myAmount;
            double myPrice;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out orderItemChoice))
            {
                switch (orderItemChoice)
                {
                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the orders's id: ");//getting details from user
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");//throw if not valid
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");//throw if not valid
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");//throw if not valid
                        Console.WriteLine("Enter amount: ");
                        if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect amount");//throw if not valid
                        OrderItem oi = new OrderItem // initalize the new OrderItem
                        {
                            OrderId = myOrderId,
                            ProductId = myProductId,
                            Price = myPrice,
                            Amount = myAmount,
                        };
                        dal.OrderItem.Add(oi);//add to array
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the product-order's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                        dal.OrderItem.Delete(myId);
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the product-order's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Console.WriteLine(dal.OrderItem.GetById(myId));//find the product we want to update
                        Console.WriteLine("Enter product-order's details to update: "); //geting changes from user
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine("Enter amount: ");
                        if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect amount");
                        OrderItem oi1 = new OrderItem //updating the changes
                        {
                            ID = dal.OrderItem.GetById(myId).ID,
                            OrderId = myOrderId,
                            ProductId = myProductId,
                            Price = myPrice,
                            Amount = myAmount,
                        };
                        dal.OrderItem.Update(oi1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the product-order's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                        OrderItem oi2 = dal.OrderItem.GetById(myId);//find the OrderItem with the wanted id
                        Console.WriteLine(oi2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the products: ");
                        IEnumerable<OrderItem?> newOrderItemArr = dal.OrderItem.GetAll();
                        foreach (var oi3 in newOrderItemArr)
                        {
                            Console.WriteLine(oi3);//ptint all the OrderItem in the array
                        }
                        break;
                    case SecondaryChoice.GetAllOrderProducts:
                        Console.WriteLine("Enter the order's id");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        IEnumerable<OrderItem?> newAllOrderProductsArr = dal.OrderItem.GetAll(x=> x?.OrderId == myId);// find the OrderItems of the wanted order
                        foreach (var oi4 in newAllOrderProductsArr)
                        {
                            Console.WriteLine(oi4);//ptint all the OrderItems in the same order
                        }
                        break;
                    case SecondaryChoice.GetByProductIdAndOrderId:
                        Console.WriteLine("Enter the orders's id: ");//get the wanted order id
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");
                        Console.WriteLine("Enter the product's id: ");//get the wanted product id
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");
                        OrderItem oi5 = dal.OrderItem.GetByFilter(x=> x?.ProductId == myProductId && x?.OrderId == myOrderId);//find the wanted OrderItem
                        Console.WriteLine(oi5);
                        break;

                    default: throw new Exception("Error");
                }
            }
            else
            {
                throw new Exception("incorrect choice");
            }
        }
        catch (Exception newException)
        {
            Console.WriteLine(newException.ToString());
        }
    }
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello!");
        MainChoice choice = MainChoice.Order;
        do
        {
            try
            {
                Console.WriteLine(@"
                            please enter your choice:
                            0 for End
                            1 for Product
                            2 for Order
                            3 for OrderItem");
                if (MainChoice.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case MainChoice.End:
                            Console.WriteLine("GoodBye!");
                            break;
                        case MainChoice.Product:
                            productFunc();
                            break;
                        case MainChoice.Order:
                            orderFunc();
                            break;
                        case MainChoice.OrderItem:
                            orderItemFunc();
                            break;
                        default: throw new Exception("Error");
                    }
                }
                else
                {
                    throw new Exception("incorrect choice");
                }
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.ToString());
            }
        }
        while (choice != MainChoice.End);
    }
}