using BO;
namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList?> GetProductsList();// get list of products
    BO.Product GetProductDitailesManager(int id);// get by id for manager
    BO.ProductItem GetProductDitailes(int id, BO.Cart c);// get by id
    void AddProduct(int id, string name, double price, int amount, BO.Category c);// add product
    void DeleteProduct(int id);// delete product
    void UpdateProduct(BO.Product p);// update product
}
