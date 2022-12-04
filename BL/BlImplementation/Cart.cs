using BlApi;
using BO;
using Dal;
using DalApi;
using DO;


namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
   public BO.Cart AddItem(BO.Cart c, int productId)
    {

        BO.OrderItem?oi = (from  item in c.Items
                           where item.ProductId == productId
                           select item).FirstOrDefault();
        DO.Product p = dal.Product.GetById(productId);

        if (oi == null)
        {
            if (p.InStock > 0)
            {
                c.TotalPrice += p.Price;
                c.Items.Append(new BO.OrderItem
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
            oi.Amount++;
            oi.TotalPrice += oi.Price;//update the total price
            c.TotalPrice += p.Price;
        }
        else
            throw new Exception("not in stock");

        return c;
    }

    public void ConfirmCart(BO.Cart c, string name, string address, string email)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateItem(BO.Cart c, int productId, int amount)
    {
        try
        {
            DO.Product p = dal.Product.GetById(productId);
            BO.OrderItem? oi = (from item in c.Items
                                where item.ProductId == productId
                                select item).First();
            if (amount == 0)
            {
                c.Items.ToList().Remove(oi);   
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
                    //throw
            }
            else if(amount < oi.Amount)
            {
                c.TotalPrice -= oi.Price * (oi.Amount - amount);
                oi.TotalPrice -= oi.Price * (oi.Amount - amount);
                oi.Amount = amount;
            }
        }


    }
}
