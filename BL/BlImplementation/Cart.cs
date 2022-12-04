using BlApi;
using Dal;

namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
   public BO.Cart AddItem(BO.Cart c, int productId)
    {
           if( DO.OrderItem oi = dal.OrderItem.GetById(productId))
          {
            from BO.OrderItem item in c.Items
            where item.ProductId == productId
            select 
          }
            if(P.InStock > 0)

 

    }

    public void ConfirmCart(BO.Cart c, string name, string address, string email)
    {
        throw new NotImplementedException();
    }

    public BO.Cart UpdateItem(BO.Cart c, int productId, int amount)
    {
        try
        { DO.Product P = dal.Product.GetById(productId);
        if(P.InStock == 0)


    }
}
