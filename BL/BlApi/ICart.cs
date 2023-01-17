using BO;
namespace BlApi;

public interface ICart 
{
    Cart AddItem(Cart c, int productId);// add item to a cart
    Cart UpdateItem(Cart c, int productId, int amount);// update amount of product in cart
    int ConfirmCart(Cart c, string name, string address, string email);// confirm cart
}
