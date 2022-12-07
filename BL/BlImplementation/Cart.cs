using BlApi;


namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
   public BO.Cart AddItem(BO.Cart c, int productId)
    {
        try
        {

            BO.OrderItem? oi = (from item in c.Items
                                where item.ProductId == productId
                                select item).FirstOrDefault();
            DO.Product p = dal.Product.GetById(productId);
            if (oi == null)
            {
                if (p.InStock >= 0)
                {
                    c.TotalPrice += p.Price;
                    ((List<BO.OrderItem?>)c.Items!).Add(new BO.OrderItem
                    {
                        Name = p.Name,
                        ProductId = p.ID,
                        Amount = 1,
                        Price = p.Price,
                        TotalPrice = p.Price
                    });
                }
            }
            else if (p.InStock >= oi.Amount + 1)
            {
                oi.Price = p.Price;
                oi.Amount++;
                oi.TotalPrice += oi.Price;//update the total price 
            }
            else
            {
                throw new BO.BlNotInStockException(p.Name!, p.ID);
            }

            return c;
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Cart doesn't exist", exception);
        }
    }

    public void ConfirmCart(BO.Cart c, string name, string address, string email)
    {
        try
        {
            foreach (BO.OrderItem oi in c.Items!)
            {
                DO.Product p = dal.Product.GetById(oi.Id);
                if (oi.Amount > p.InStock)
                    throw new BO.BlNotInStockException(p.Name!, p.ID);
                if (oi.Amount <= 0)
                    throw new BO.BlInvalidExspressionException("Amount");
            }
            if (c.CostomerEmail == null)
                throw new BO.BlNullPropertyException("Email");
            if (c.CostomerName == null)
                throw new BO.BlNullPropertyException("Name");
            if (c.CostomerAddress == null)
                throw new BO.BlNullPropertyException("Address");
            int orderid = dal.Order.Add(new DO.Order()
            {
                CustomerName = c.CostomerName,
                CustomerAddress = c.CostomerAddress,
                CustomerEmail = c.CostomerEmail,
                DeliveryDate = null,
                OrderDate = DateTime.MinValue,
                ShipDate = null,
            });
            foreach (BO.OrderItem oi in c.Items)
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
                product.InStock -= orderitem.Amount;
                dal.Product.Update(product);
            }
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Cart doesn't exist", exception);
        }

    }

    public BO.Cart UpdateItem(BO.Cart c, int productId, int amount)
    {
        try
        {
         if(productId < 100000)
            {
                throw new BO.BlInvalidExspressionException("Id");
            }
            if (productId < 0)
            {
                throw new BO.BlInvalidExspressionException("Amount");
            }

            DO.Product p = dal.Product.GetById(productId);
            BO.OrderItem? oi = (from item in c.Items
                                where item.ProductId == productId
                                select item).First();
            if (amount == 0)
            {
                ((List<BO.OrderItem?>)c.Items!).Remove(oi);
                c.TotalPrice -= oi.Price;
            }
            else if (amount > oi.Amount)
            {
                if (p.InStock >= amount)
                {
                    c.TotalPrice += oi.Price * (amount - oi.Amount);
                    oi.TotalPrice += oi.Price * (amount - oi.Amount);
                    oi.Amount = amount;
                }
                else
                {
                    throw new BO.BlNotInStockException(oi.Name!, oi.Id);
                }
            }
            else if (amount < oi.Amount)
            {
                c.TotalPrice -= oi.Price * (oi.Amount - amount);
                oi.TotalPrice -= oi.Price * (oi.Amount - amount);
                oi.Amount = amount;
            }
            return c;
        }
        catch (DO.DalMissingIdException exception)
        {
            throw new BO.BlMissingEntityException("Cart doesn't exist", exception);
        }

    }
}
