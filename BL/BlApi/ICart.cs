using BO;
namespace BlApi;

public interface ICart 
{
    Cart AddItem(Cart c, int productId);// add item to a cart
    Cart UpdateItem(Cart c, int productId, int amount);// update amount og product in cart
    void ConfirmCart(Cart c, string name, string address, string email);// confirm cart
}
