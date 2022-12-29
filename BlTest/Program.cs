using BlApi;
using BlImplementation;
using BO;
namespace BlTest
{
    internal class Program
    {
        #region Choices
        public enum MainChoice { End=0, Product, Order, Cart}// choices for the main switch
        public enum productChoice { Add = 1, Delete, Update, GetProductsList, GetProductDitailes, GetProductDitailesManager }// choices for the Product switch
        public enum orderChoice { GetOrderDitails = 1, GetOrders, GetOrderTracking, UpdateIfProvided, UpdateShipping }// choices for the Order switch
        public enum cartChoice { Add = 1, ConfirmCart, UpdateItem }// choices for the Cart switch
        #endregion
        private static IBl bl = BlApi.Factory.Get();
        #region productFunction
        private static void ProductFunc(Cart c)
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
                productChoice productChoice = productChoice.Add;// initalizing the choise
                int myId, myAmount, myInStock;// all the needed details
                string? myName;
                double myPrice;
                Category myCategory;
                if (productChoice.TryParse(Console.ReadLine(), out productChoice))
                {
                    switch (productChoice)
                    {
                        case productChoice.Add:
                            Console.WriteLine("Enter the product's id: ");// getting details from user
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            Console.WriteLine("Enter the product's name: ");
                            myName = Console.ReadLine();
                            Console.WriteLine("Enter the product's price: ");
                            if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new BlInvalidExspressionException("Price");// if not valid
                            Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2 - BRUSHES
                                           3 - LIPS
                                           4 - BEAUTY");
                            if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 4 || (int)myCategory < 0) throw new BlWrongCategoryException();// if not valid category
                            Console.WriteLine("Enter the amount of products in stock: ");
                            if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new BlInvalidExspressionException("Amount");// if not valid
                            BO.Product p = new BO.Product // initalizing the new product
                            {
                                Id = myId,
                                Name = myName,
                                Price = myPrice,
                                Category = myCategory,
                                InStock = myInStock,
                            };
                            bl.Product.AddProduct(p.Id, p.Name!, p.Price, p.InStock, p.Category);// adding the product
                            break;


                        case productChoice.Delete:
                            Console.WriteLine("Enter the product's id to delete: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");//if not valid
                            bl.Product.DeleteProduct(myId);// deleting the product
                            break;


                        case productChoice.Update:
                            Console.WriteLine("Enter the product's id to update: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");//if not valid
                            Console.WriteLine(bl.Product.GetProductDitailes(myId, c));// find the product we want to update
                            Console.WriteLine("Enter product's details to update: ");// recive the changes we want to update
                            Console.WriteLine("Enter the product's name: ");
                            myName = Console.ReadLine();
                            if (myName == null)// if name is null
                            {
                                throw new BlNullPropertyException("Name");
                            }
                            Console.WriteLine("Enter the product's price: ");
                            if (double.TryParse(Console.ReadLine(), out myPrice) == false) throw new BlInvalidExspressionException("Price");// if not valid
                            Console.WriteLine(@"Enter the product's category:
                                           0 - EYES
                                           1 - FACE
                                           2 - BRUSHES
                                           3 - LIPS
                                           4 - BEAUTY");
                            if (Category.TryParse(Console.ReadLine(), out myCategory) == false || (int)myCategory > 4 || (int)myCategory < 0) throw new BlWrongCategoryException();// if not valid category
                            Console.WriteLine("Enter the amount of products: ");
                            if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new BlInvalidExspressionException("Amount");// if not valid
                            Console.WriteLine("Enter the amount of products in stock: ");
                            if (int.TryParse(Console.ReadLine(), out myInStock) == false) throw new BlInvalidExspressionException("Stock");// if not valid
                            Product p1 = new Product // updating the product's details
                            {
                                Id = myId,
                                Name = myName,
                                Price = myPrice,
                                Category = myCategory,
                                InStock = myInStock
                            };
                            bl.Product.UpdateProduct(p1);// updating the product
                            break;


                        case productChoice.GetProductsList:
                            Console.WriteLine("all of the products: ");
                            IEnumerable<ProductForList?> newProductList = bl.Product.GetProductsList();// getting all the products
                            foreach (var p5 in newProductList)
                            {
                                Console.WriteLine(p5);// printing all the producs
                            }
                            break;

                        case productChoice.GetProductDitailes:
                            Console.WriteLine("Enter the product's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            BO.ProductItem p2 = bl.Product.GetProductDitailes(myId, c);// finding the wanted product
                            Console.WriteLine(p2);
                            break;


                        case productChoice.GetProductDitailesManager:
                            Console.WriteLine("Enter the product's id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            BO.Product p3 = bl.Product.GetProductDitailesManager(myId);// finding the wanted product
                            Console.WriteLine(p3);
                            break;
                        default: throw new BlWrongChoiceException();// wrong choice 
                    }
                }
                else
                {
                    throw new BlWrongChoiceException();// unknown choice
                }
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.ToString());
            }
        }
        #endregion productFunction
        #region orderFunction
        private static void OrderFunc()
        {
            Console.WriteLine(@"please enter your choice:
                                1 - getting an order by its id
                                2 - getting a list of orders
                                3 - getting orderTracking
                                4 - updating an order if provided
                                5 - updating an order if shipped");
            try
            {
                orderChoice orderChoice = orderChoice.UpdateIfProvided;// initalizing the choise
                int myId;

                if (orderChoice.TryParse(Console.ReadLine(), out orderChoice))
                {
                    switch (orderChoice)
                    {

                        case orderChoice.GetOrderDitails:
                            Console.WriteLine("Enter the order id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            Order o4 = bl.Order.GetOrderDitailes(myId);// getting the order's details
                            Console.WriteLine(o4);
                            break;


                        case orderChoice.GetOrders:
                            Console.WriteLine("all of the orders: ");
                            IEnumerable<OrderForList?> newOrderArr = bl.Order.GetOrders();// getting all the orders
                            foreach (var o5 in newOrderArr)
                            {
                                Console.WriteLine(o5);// printing all the orders
                            }
                            break;


                        case orderChoice.GetOrderTracking:
                            Console.WriteLine("Enter the order id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            OrderTracking o1 = bl.Order.GetOrderTracking(myId);// getting the order's tracking details
                            Console.WriteLine(o1);
                            break;


                        case orderChoice.UpdateIfProvided:
                            Console.WriteLine("Enter the order id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            Order o3 = bl.Order.UpdateIfProvided(myId);// updating the order's status
                            Console.WriteLine(o3);
                            break;


                        case orderChoice.UpdateShipping:
                            Console.WriteLine("Enter the order id: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");// if not valid
                            Order o2 = bl.Order.UpdateShipping(myId);// updating the order's status
                            Console.WriteLine(o2);
                            break;

                        default: throw new BlWrongChoiceException();// wrong choice
                    }
                }
                else
                {
                    throw new BlWrongChoiceException();// unknown choice
                }
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.ToString());
            }
        }
        #endregion orderFunction
        #region cartFunction
        private static void CartFunc(Cart c)
        {
            Console.WriteLine(@"please enter your choice:
                                1 - adding a product to a cart
                                2 - confirm your cart
                                3 - updating amount of an item");
            try
            {
                cartChoice cChoice = cartChoice.Add;// initalizing the choise
                int myId, myAmount;
                string? myCustomerName, myCustomerEmail, myCustomerAddress;
                if (cartChoice.TryParse(Console.ReadLine(), out cChoice))
                {
                    switch (cChoice)
                    {
                        case cartChoice.Add:
                            Console.WriteLine("Enter the product's id to add: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");//throw if not valid
                            c = bl.Cart.AddItem(c, myId);//adding
                            Console.WriteLine(c);
                            break;

                        case cartChoice.ConfirmCart:
                            Console.WriteLine("Enter the costumer's name: ");    //getting details from user
                            myCustomerName = Console.ReadLine();
                            if (myCustomerName == null )
                            {
                                throw new BlNullPropertyException("Name");
                            }
                            Console.WriteLine("Enter the costumer's address: ");
                            myCustomerAddress = Console.ReadLine();
                            if (myCustomerAddress == null)
                            {
                                throw new BlNullPropertyException("Address");
                            }
                            Console.WriteLine("Enter the costumer's email: ");
                                myCustomerEmail = Console.ReadLine();
                            if (myCustomerEmail == null)
                            {
                                throw new BlNullPropertyException("Email");
                            }
                            if (!myCustomerEmail.Contains('@'))
                            {
                                throw new BO.BlInvalidExspressionException("Email");
                            }
                            bl.Cart.ConfirmCart(c, myCustomerName!, myCustomerAddress!, myCustomerEmail!);
                            break;


                        case cartChoice.UpdateItem:
                            Console.WriteLine("Enter the product's id to update: ");
                            if (int.TryParse(Console.ReadLine(), out myId) == false) throw new BlInvalidExspressionException("Id");//throw if not valid
                            Console.WriteLine("Enter the amount: ");
                            if (int.TryParse(Console.ReadLine(), out myAmount) == false) throw new BlInvalidExspressionException("Id");//throw if not valid    
                            c = bl.Cart.UpdateItem(c, myId, myAmount);
                            Console.WriteLine(c);
                            break;

                        default: throw new BlWrongChoiceException();
                    }
                }
                else
                {
                    throw new BlWrongChoiceException();// unknown choice
                }
            }
            catch (Exception newException)
            {
                Console.WriteLine(newException.ToString());
            }
        }
        #endregion cartFunction
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
                                ProductFunc(c);
                                break;
                            case MainChoice.Order:
                                OrderFunc();
                                break;
                            case MainChoice.Cart:
                                CartFunc(c);
                                break;
                            default: throw new BlWrongChoiceException();
                        }
                    }
                    else
                    {
                        throw new BlWrongChoiceException();// unknown choice
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
