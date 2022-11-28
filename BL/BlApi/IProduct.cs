using BO;
namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList?> GetProductsList();
    IEnumerable<ProductItem?> GetProducts();
    Product GetProsuctDitailesManager(int id);
    Product GetProsuctDitailes(int id, Cart c);
    Cart AddProduct(int id, string name, double price, int amount);
    Cart DeleteProduct(int id);
    Cart UpdateProduct(int id, string name, double price, int amount);






}
