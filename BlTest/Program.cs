using BlApi;
using BlImplementation;
using BO;
using DalApi;

namespace BlTest
{
    internal class Program
    {
        public enum MainChoice { End=0, Product, Order, Cart}
        public enum productChoice { Add = 1, Delete, Update, /*GetProducts,*/ GetProductsList, GetProductDitailes, GetProductDitailesManager }
        public enum orderChoice { GetOrderDitails = 1, GetOrders, GetOrderTracking, UpdateIfProvided, UpdateShipping }
        public enum cartChoice { Add = 1, ConfirmCart, UpdateItem }

        private static IBl bl = new Bl();
        private static void productFunc(Cart c)
        {
            Console.WriteLine(@"please enter your choice:
                            1 - adding a product
                            2 - deleting a product
                            3 - updating a product
                            4 - getting a list of products
                            5 - getting product ditails
                            6 - getting product ditails, manager");
            try
            {
                productChoice productChoice = productChoice.Add;//initalize the choise
                int myId, myAmount, myInStock;
                string? myName;
                double myPrice;
                Category myCategory;
                if (productChoice.TryParse(Console.ReadLine(), out productChoice))
                {
                    switch (productChoice)
                    {
                        case productChoice.Add:
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
                            BO.Product p = new BO.Product //initalize the new product
                            {
                                Id = myId,
                                Name = myName,
                                Price = myPrice,
                                Category = myCategory,
                                InStock = myInStock,
                            };
                            bl.Product.AddProduct(p.Id, p.Name, p.Price, p.InStock);
                            break;


                        case productChoice.Delete:
                            Console.WriteLine("Enter the product's id to delete: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//if not valid
                            bl.Product.DeleteProduct(myId);
                            break;


                        case productChoice.Update:
                            Console.WriteLine("Enter the product's id to update: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//if not valid
                            Console.WriteLine(bl.Product.GetProductDitailes(myId, c)); //find the product we want to update
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
                            Console.WriteLine("Enter the amount of products: ");
                            if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect amount");
                            Console.WriteLine("Enter the amount of products in stock: ");
                            if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new Exception("incorrect stock");
                            Product p1 = new Product //updating the changes the user gave
                            {
                                Id = myId,
                                Name = myName,
                                Price = myPrice,
                                Category = myCategory,
                                InStock = myInStock
                            };
                            bl.Product.UpdateProduct(p1);
                            break;


                        //case productChoice.GetProducts:
                        //    Console.WriteLine("all of the products: ");
                        //    IEnumerable<ProductItem?> newProductArr = bl.Product.GetProducts();
                        //    foreach (var p4 in newProductArr)
                        //    {
                        //        Console.WriteLine(p4);//print all the producs
                        //    }
                        //    break;


                        case productChoice.GetProductsList:
                            Console.WriteLine("all of the products: ");
                            IEnumerable<ProductForList?> newProductList = bl.Product.GetProductsList();
                            foreach (var p5 in newProductList)
                            {
                                Console.WriteLine(p5);//print all the producs
                            }
                            break;

                        case productChoice.GetProductDitailes:
                            Console.WriteLine("Enter the product's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                            BO.ProductItem p2 = bl.Product.GetProductDitailes(myId, c);// find the wanted product
                            Console.WriteLine(p2);
                            break;


                        case productChoice.GetProductDitailesManager:
                            Console.WriteLine("Enter the product's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                            BO.Product p3 = bl.Product.GetProductDitailesManager(myId);// find the wanted product
                            Console.WriteLine(p3);
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
                            1 - getting an order by its id
                            2 - getting a list of orders
                            3 - getting orderTracking
                            4 - updating an order if provided
                            5 - updating an order if shipped");
            try
            {
                orderChoice orderChoice = orderChoice.UpdateIfProvided;//initalize the choise
                int myId;

                if (orderChoice.TryParse(Console.ReadLine(), out orderChoice))
                {
                    switch (orderChoice)
                    {

                        case orderChoice.GetOrderDitails:
                            Console.WriteLine("Enter the orders's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//invalid input
                            Order o4 = bl.Order.GetOrderDitailes(myId);
                            Console.WriteLine(o4);
                            break;


                        case orderChoice.GetOrders:
                            Console.WriteLine("all of the orders: ");
                            IEnumerable<OrderForList?> newOrderArr = bl.Order.GetOrders();
                            foreach (var o5 in newOrderArr)
                            {
                                Console.WriteLine(o5); // print all the orders in the array
                            }
                            break;


                        case orderChoice.GetOrderTracking:
                            Console.WriteLine("Enter the orders's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//invalid input
                            OrderTracking o1 = bl.Order.GetOrderTracking(myId);
                            Console.WriteLine(o1);
                            break;


                        case orderChoice.UpdateIfProvided:
                            Console.WriteLine("Enter the orders's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//invalid input
                            Order o3 = bl.Order.UpdateIfProvided(myId);
                            Console.WriteLine(o3);
                            break;


                        case orderChoice.UpdateShipping:
                            Console.WriteLine("Enter the orders's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//invalid input
                            Order o2 = bl.Order.UpdateShipping(myId);
                            Console.WriteLine(o2);
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
        private static void cartFunc(Cart c)
        {
            Console.WriteLine(@"please enter your choice:
                            1 - adding a cart
                            2 - confirm your cart
                            3 - updating amount of item");
            try
            {
                cartChoice cChoice = cartChoice.Add;//initalize the choise
                int myId, myAmount;
                string? myCustomerName, myCustomerEmail, myCustomerAddress;
                if (cartChoice.TryParse(Console.ReadLine(), out cChoice))
                {
                    switch (cChoice)
                    {
                        case cartChoice.Add:
                            Console.WriteLine("Enter the product's id to add: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                            c = bl.Cart.AddItem(c, myId);//adding
                            Console.WriteLine(c);
                            break;

                        case cartChoice.ConfirmCart:
                            Console.WriteLine("Enter the costumer's name: ");//getting details from user
                            myCustomerName = Console.ReadLine();
                            Console.WriteLine("Enter the costumer's address: ");
                            myCustomerAddress = Console.ReadLine();
                            Console.WriteLine("Enter the costumer's email: ");
                            myCustomerEmail = Console.ReadLine();
                            bl.Cart.ConfirmCart(c, myCustomerName!, myCustomerAddress!, myCustomerEmail!);
                            break;


                        case cartChoice.UpdateItem:
                            Console.WriteLine("Enter the product's id to update: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new Exception("incorrect id");//throw if not valid
                            Console.WriteLine("Enter the product's amount: ");
                            if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new Exception("incorrect id");//throw if not valid    
                            c = bl.Cart.UpdateItem(c, myId, myAmount);
                            Console.WriteLine(c);
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

        static void Main(string[] args)
        {
            BO.Cart c = new BO.Cart()
            {
                CostomerName = "Avi Cohen",
                CostomerAddress = "David 10",
                CostomerEmail = "Avi.Cohen@gmail.com",
                Items = new List<BO.OrderItem>(),
                TotalPrice = 0,
            };
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
                            3 for Cart");
                    if (MainChoice.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case MainChoice.End:
                                Console.WriteLine("GoodBye!");
                                break;
                            case MainChoice.Product:
                                productFunc(c);
                                break;
                            case MainChoice.Order:
                                orderFunc();
                                break;
                            case MainChoice.Cart:
                                cartFunc(c);
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
}