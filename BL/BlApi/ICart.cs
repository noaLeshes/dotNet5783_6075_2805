using BO;
namespace BlApi;

public interface ICart 
{
    Cart AddItem(Cart c, int productId);
    Cart UpdateItem(Cart c, int productId, int amount);
    void ConfirmCart(Cart c, string name, string address, string email);
}
