using BlApi;

namespace BlImplementation;

internal class Cart : ICart
{
    BO.Cart ICart.AddItem(BO.Cart c, int productId)
    {

        throw new NotImplementedException();
    }

    void ICart.ConfirmCart(BO.Cart c, string name, string address, string email)
    {
        throw new NotImplementedException();
    }

    BO.Cart ICart.UpdateItem(BO.Cart c, int productId, int amount)
    {
        throw new NotImplementedException();
    }
}
