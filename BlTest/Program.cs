using BlApi;
using BlImplementation;
using BO;
using DalApi;

namespace BlTest
{
    internal class Program
    {
        public enum productChoice { Add = 1, Delete, Update, GetProducts, GetProductsList, GetProductDitailes, GetProductDitailesManager }

        private static IBl bl = new Bl();
        private static void productFunc()
        {
            Console.WriteLine(@"please enter your choice:
                            1 - adding a product
                            2 - deleting a product
                            3 - updating a product
                            4 - getting all the products
                            5 - getting a list of products
                            6 - getting product ditails
                            7 - getting product ditails, manager");
            try
            {
                productChoice productChoice = productChoice.Add;//initalize the choise
                int myId, myAmount, myInStock;
                string? myName;
                double myPrice;
                Category myCategory;
                Cart c = new();
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


                        case productChoice.GetProducts:
                            Console.WriteLine("all of the products: ");
                            IEnumerable<ProductItem?> newProductArr = bl.Product.GetProducts();
                            foreach (var p4 in newProductArr)
                            {
                                Console.WriteLine(p4);//print all the producs
                            }
                            break;


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
        static void Main(string[] args)
        {
           
        }
    }
}