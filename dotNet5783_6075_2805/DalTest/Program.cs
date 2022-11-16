using DO;
using Dal;
using System.Xml.Linq;
using System;
using System.Data;

public enum MainChoice { End=0, Product, Order, OrderItem }
public enum SecondaryChoice { Add=1, Delete, Update, GetById, GetAll, GetAllOrderProducts, GetByProductIdAndOrderId }
internal class Program
{
    private static DalProduct dalProduct = new DalProduct();
    private static DalOrder dalOrder = new DalOrder();
    private static DalOrderItem dalOrderItem = new DalOrderItem();

    private static void ProductFunc()
    {
        Console.WriteLine(@"please enter your choice:
                            1 - adding a product
                            2 - deleting a product
                            3 - updating a product
                            4 - getting a product by its id
                            5 - getting all the products");
        try
        {
            SecondaryChoice productChoice = SecondaryChoice.Add;
            int myId, myInStock;
            string? myName;
            double myPrice;
            Category myCategory;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out productChoice))
            {
                switch (productChoice)
                {

                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Console.WriteLine("Enter the product's name: ");
                        myName = Console.ReadLine();
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2- LIPS
                                           3 - BRUSHES
                                           4 - BEAUTY");
                        if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 5 || (int)myCategory < 0) throw new Exception("incorrect category");
                        Console.WriteLine("Enter the amount of products in stock: ");
                        if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new Exception("incorrect amount");
                        Product p = new Product
                        {
                            ID = myId,
                            Name = myName,
                            Price = myPrice,
                            Category = myCategory,
                            InStock = myInStock,
                        };
                        dalProduct.Add(p);
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the product's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        dalProduct.Delete(myId);
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the product's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Console.WriteLine(dalProduct.GetById(myId));
                        Console.WriteLine("Enter product's details to update: ");
                        Console.WriteLine("Enter the product's name: ");
                        myName = Console.ReadLine();
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2- LIPS
                                           3 - BRUSHES
                                           4 - BEAUTY");
                        if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 5 || (int)myCategory < 0) throw new Exception("incorrect category");
                        Console.WriteLine("Enter the amount of products in stock: ");
                        if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new Exception("incorrect amount");
                        Product p1 = new Product
                        {
                            ID = myId,
                            Name = myName,
                            Price = myPrice,
                            Category = myCategory,
                            InStock = myInStock,
                        };
                        dalProduct.Update(p1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Product p2 = dalProduct.GetById(myId);
                        Console.WriteLine(p2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the products: ");
                        Product[] newProductArr = dalProduct.GetAll();
                        foreach (Product p3 in newProductArr)
                        {
                            Console.WriteLine(p3);
                        }
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
    private static void OrderFunc()
    {
        Console.WriteLine(@"please enter your choice:
                            1 - adding an order
                            2 - deleting an order
                            3 - updating an order
                            4 - getting an order by its id
                            5 - getting all the orders");
        try
        {
            SecondaryChoice orderChoice = SecondaryChoice.Add;
            int myId;
            string? myCustomerName, myCustomerEmail, myCustomerAddress;
            DateTime myOrderDate/*, myShipDate, myDeliveryDate*/;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out orderChoice))
            {
                switch (orderChoice)
                {


                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the costumer's name: ");
                        myCustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's email: ");
                        myCustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's address: ");
                        myCustomerAddress = Console.ReadLine();
                        myOrderDate = DateTime.Now;
                        Order o = new Order
                        {
                            CustomerName = myCustomerName,
                            CustomerEmail = myCustomerEmail,
                            CustomerAddress = myCustomerAddress,
                            OrderDate = myOrderDate,
                            ShipDate = /*null*/myOrderDate.AddHours(10),
                            DeliveryDate = /*null*/myOrderDate.AddDays(2),
                        };
                        dalOrder.Add(o);
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the order's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        dalOrder.Delete(myId);
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the order's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Console.WriteLine(dalOrder.GetById(myId));
                        Console.WriteLine("Enter product's details to update: ");
                        Console.WriteLine("Enter the costumer's name: ");
                        myCustomerName = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's email: ");
                        myCustomerEmail = Console.ReadLine();
                        Console.WriteLine("Enter the costumer's address: ");
                        myCustomerAddress = Console.ReadLine();
                        Console.WriteLine("Enter the order's date: ");
                        if (DateTime.TryParse(Console.ReadLine(), out myOrderDate) == false) throw new Exception("incorrect id");

                        Order o1 = new Order
                        {
                            ID = dalOrder.GetById(myId).ID,
                            CustomerName = myCustomerName,
                            CustomerEmail = myCustomerEmail,
                            CustomerAddress = myCustomerAddress,
                            OrderDate = /*dalOrder.GetById(myId).OrderDate*/ myOrderDate,
                            ShipDate = myOrderDate.AddHours(10),
                            DeliveryDate = myOrderDate.AddDays(2),
                        };
                        dalOrder.Update(o1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Order o2 = dalOrder.GetById(myId);
                        Console.WriteLine(o2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the orders: ");
                        Order[] newOrderArr = dalOrder.GetAll();
                        foreach (Order o3 in newOrderArr)
                        {
                            Console.WriteLine(o3);
                        }
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
    private static void OrderItemFunc()
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
            SecondaryChoice orderItemChoice = SecondaryChoice.Add;
            int myId, myOrderId, myProductId, myAmount;
            double myPrice;
            if (SecondaryChoice.TryParse(Console.ReadLine(), out orderItemChoice))
            {
                switch (orderItemChoice)
                {


                    case SecondaryChoice.Add:
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine("Enter amount: ");
                        if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect amount");
                        OrderItem oi = new OrderItem
                        {
                            OrderId = myOrderId,
                            ProductId = myProductId,
                            Price = myPrice,
                            Amount = myAmount,
                        };
                        dalOrderItem.Add(oi);
                        break;


                    case SecondaryChoice.Delete:
                        Console.WriteLine("Enter the product-order's id to delete: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        dalOrderItem.Delete(myId);
                        break;


                    case SecondaryChoice.Update:
                        Console.WriteLine("Enter the product-order's id to update: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        Console.WriteLine(dalOrderItem.GetById(myId));
                        Console.WriteLine("Enter product-order's details to update: ");
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");
                        Console.WriteLine("Enter the product's price: ");
                        if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new Exception("incorrect price");
                        Console.WriteLine("Enter amount: ");
                        if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect amount");
                        OrderItem oi1 = new OrderItem
                        {
                            ID = dalOrderItem.GetById(myId).ID,
                            OrderId = myOrderId,
                            ProductId = myProductId,
                            Price = myPrice,
                            Amount = myAmount,
                        };
                        dalOrderItem.Update(oi1);
                        break;


                    case SecondaryChoice.GetById:
                        Console.WriteLine("Enter the product-order's id: ");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        OrderItem oi2 = dalOrderItem.GetById(myId);
                        Console.WriteLine(oi2);
                        break;


                    case SecondaryChoice.GetAll:
                        Console.WriteLine("all of the products: ");
                        OrderItem[] newOrderItemArr = dalOrderItem.GetAll();
                        foreach (OrderItem oi3 in newOrderItemArr)
                        {
                            Console.WriteLine(oi3);
                        }
                        break;


                    case SecondaryChoice.GetAllOrderProducts:
                        Console.WriteLine("Enter the order's id");
                        if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");
                        OrderItem[] newAllOrderProductsArr = dalOrderItem.GetAllOrderProducts(myId);
                        foreach (OrderItem oi4 in newAllOrderProductsArr)
                        {
                            Console.WriteLine(oi4);
                        }
                        break;


                    case SecondaryChoice.GetByProductIdAndOrderId:
                        Console.WriteLine("Enter the orders's id: ");
                        if (int.TryParse(Console.ReadLine(), out myOrderId) == false) throw new Exception("incorrect order id");
                        Console.WriteLine("Enter the product's id: ");
                        if (int.TryParse(Console.ReadLine(), out myProductId) == false) throw new Exception("incorrect product id");
                        OrderItem oi5 = dalOrderItem.GetByProductIdAndOrderId(myOrderId, myProductId);
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
        MainChoice choice = MainChoice.Order;
        do
        {
            try
            {
                Console.WriteLine(@"Hello! 
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
                            ProductFunc();
                            break;
                        case MainChoice.Order:
                            OrderFunc();
                            break;
                        case MainChoice.OrderItem:
                            OrderItemFunc();
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