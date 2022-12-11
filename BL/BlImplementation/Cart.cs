using BlApi;
using DalApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Cart : ICart
{
    private static int index_orderItem =1000000;//  running number for when we create a new orderItem
    DalApi.IDal dal = new Dal.DalList();
   public BO.Cart AddItem(BO.Cart c, int productId)
    {
        try
        {

            BO.OrderItem? oi = (from item in c.Items// finding an orderItem
                                where item.ProductId == productId
                                select item).FirstOrDefault();
            DO.Product p = dal.Product.GetById(productId);// finding a product
            if (oi == null)// if not found
            {
                if (p.InStock >= 0)// if the wanted product id in stock
                {
                    c.TotalPrice += p.Price;
                    ((List<BO.OrderItem?>)c.Items!).Add(new BO.OrderItem// adding a new product
                    {
                        Id = index_orderItem,
                        Name = p.Name,
                        ProductId = p.ID,
                        Amount = 1,
                        Price = p.Price,
                        TotalPrice = p.Price
                    });
                    p.InStock -= 1;// updating the amount of product in stock
                    dal.Product.Update(p);// update the produt stock
                    index_orderItem++;
                }
            }
            else if (p.InStock >= oi.Amount + 1)// if the product is found and there are enough of it in stock
            {
                oi.Price = p.Price;
                oi.Amount++;// update the amount
                oi.TotalPrice += oi.Price;// update the total price 
                c.TotalPrice += p.Price;
            }
            else// if the product is found and there aren't enough of it in stock
            {
                throw new BO.BlNotInStockException(p.Name!, p.ID);
            }

            return c;
        }
        catch (DO.DalMissingIdException exception)//if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Product doesn't exist", exception);
        }
    }

    public void ConfirmCart(BO.Cart c, string name, string address, string email)
    {
        try
        {
            //var list = from BO.OrderItem oi in c.Items// checking the cart's items
            //           let p = dal.Product.GetById(oi.Id)
            //           where oi.Amount > p.InStock
            //           select oi;
            //if (!list.Any())
            //{
            //    var p1 = from p in list
            //             select p;
            //    throw new BO.BlNotInStockException(p1, p.ID);
            //}




            //{
            //    DO.Product p = dal.Product.GetById(oi.Id);
            //    if (oi.Amount > p.InStock)// not enough in stock
            //        throw new BO.BlNotInStockException(p.Name!, p.ID);
            //    if (oi.Amount <= 0)// throwing if a detail is invalid
            //        throw new BO.BlInvalidExspressionException("Amount");
            //}
            foreach (BO.OrderItem oi in c.Items!)// checking the cart's items
            {
                DO.Product p = dal.Product.GetById(oi.Id);
                if (oi.Amount > p.InStock)// not enough in stock
                    throw new BO.BlNotInStockException(p.Name!, p.ID);
                if (oi.Amount <= 0)// throwing if a detail is invalid
                    throw new BO.BlInvalidExspressionException("Amount");
            }
            if (c.CostomerEmail == null)// throwing if a detail is invalid
                throw new BO.BlNullPropertyException("Email");
            if (c.CostomerName == null)
                throw new BO.BlNullPropertyException("Name");
            if (c.CostomerAddress == null)
                throw new BO.BlNullPropertyException("Address");
            if (!c.CostomerEmail.Contains('@'))
            {
                throw new BO.BlInvalidExspressionException("Email");
            }
            int orderid = dal.Order.Add(new DO.Order()// adding a new order
            {
                CustomerName = c.CostomerName,
                CustomerAddress = c.CostomerAddress,
                CustomerEmail = c.CostomerEmail,
                DeliveryDate = null,
                OrderDate = DateTime.Now,
                ShipDate = null,
            });
            foreach (BO.OrderItem oi in c.Items)// creating items for the order
            {
                int orderitem_id = dal.OrderItem.Add(new DO.OrderItem()
                {
                    ID = oi.Id,
                    ProductId = oi.ProductId,
                    OrderId = orderid,
                    Price = oi.Price,
                    Amount = oi.Amount
                });
                DO.OrderItem orderitem = dal.OrderItem.GetById(orderitem_id);
                DO.Product product = dal.Product.GetById(oi.ProductId);
                product.InStock -= orderitem.Amount;// updating the amount of product in stock
                dal.Product.Update(product);
            }
        }
        catch (DO.DalMissingIdException exception)//if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Cart doesn't exist", exception);
        }

    }

    public BO.Cart UpdateItem(BO.Cart c, int productId, int amount)
    {
        try
        {
            if (productId < 0)// if id is inavalid 
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            if (amount < 0)// if amount is inavalid 
            {
                throw new BO.BlInvalidExspressionException("Amount");
            }

            DO.Product p = dal.Product.GetById(productId);// getting the product
            BO.OrderItem? oi = (from item in c.Items  // finding orsderItem
                                where item.ProductId == productId
                                select item).First();
            if (amount == 0)
            {
                ((List<BO.OrderItem?>)c.Items!).Remove(oi);// if out of stock
                c.TotalPrice -= oi.Price;// updating total price
            }
            else if (amount > oi.Amount)// increasing the amount
            {
                if (p.InStock >= amount)
                {
                    c.TotalPrice += oi.Price * (amount - oi.Amount);// updating the prices and the amount
                    oi.TotalPrice += oi.Price * (amount - oi.Amount);
                    oi.Amount = amount;
                }
                else// if wanting to increase the amount and there aren't enough products in stock
                {
                    throw new BO.BlNotInStockException(oi.Name!, oi.Id);
                }
            }
            else if (amount < oi.Amount)// decreasing the amount
            {
                c.TotalPrice -= oi.Price * (oi.Amount - amount);// updating the prices and the amount
                oi.TotalPrice -= oi.Price * (oi.Amount - amount);
                oi.Amount = amount;
            }
            return c;
        }
        catch (DO.DalMissingIdException exception)//if product doesn't exist 
        {
            throw new BO.BlMissingEntityException("Cart doesn't exist", exception);
        }

    }
}
