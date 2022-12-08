using BO;
namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList?> GetProductsList();
   // IEnumerable<ProductItem?> GetProducts();
    BO.Product GetProductDitailesManager(int id);
    BO.ProductItem GetProductDitailes(int id, BO.Cart c);//get by id
    void AddProduct(int id, string name, double price, int amount, BO.Category c);
    void DeleteProduct(int id);
    void UpdateProduct(BO.Product p);
}
