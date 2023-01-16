using BO;
using DalApi;

namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductForList?> GetAllProductsByCategry(BO.Category c);
    IEnumerable<ProductForList?> GetProductsList(Func<BO.ProductForList?, bool>? filter = null);// get list of products
    public IEnumerable<ProductItem?> GetProducts(Func<BO.ProductItem?, bool>? filter = null);
    BO.Product GetProductDitailesManager(int id);// get by id for manager
    //IEnumerable<ProductForList?> GetProductsListByCategory(BO.Category c);
    BO.ProductItem GetProductDitailes(int id, BO.Cart c);// get by id
    void AddProduct(int id, string name, double price, int amount, BO.Category c);// add product
    void DeleteProduct(int id);// delete product
    void UpdateProduct(BO.Product p);// update product
    public IEnumerable<ProductForList?> PopularProducts();

}
